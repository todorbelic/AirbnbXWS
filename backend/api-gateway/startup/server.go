package startup

import (
	"airbnb/api-gateway/proto/accommodation"
	"airbnb/api-gateway/proto/reservation"
	"airbnb/api-gateway/proto/user"
	cfg "airbnb/api-gateway/startup/config"
	"context"
	"fmt"
	"github.com/gorilla/mux"
	grpc_opentracing "github.com/grpc-ecosystem/go-grpc-middleware/tracing/opentracing"
	"github.com/grpc-ecosystem/grpc-gateway/v2/runtime"
	otgo "github.com/opentracing/opentracing-go"
	muxprom "gitlab.com/msvechla/mux-prometheus/pkg/middleware"
	"google.golang.org/grpc"
	"google.golang.org/grpc/credentials/insecure"
	"log"
	"net/http"
)

type Server struct {
	config cfg.Config
	mux    *runtime.ServeMux
}

func NewServer(config cfg.Config) *Server {
	server := &Server{
		config: config,
		mux:    runtime.NewServeMux(),
	}
	server.initHandlers()
	return server
}

func (server *Server) initHandlers() {
	opts := []grpc.DialOption{
		grpc.WithTransportCredentials(insecure.NewCredentials()),
		grpc.WithUnaryInterceptor(grpc_opentracing.UnaryClientInterceptor(
			grpc_opentracing.WithTracer(otgo.GlobalTracer()))),
	}

	userEndpoint := fmt.Sprintf("localhost%s", server.config.UserServiceAddress)
	log.Println(userEndpoint)
	err := user.RegisterUserServiceRPCHandlerFromEndpoint(context.TODO(), server.mux, userEndpoint, opts)
	if err != nil {
		panic(err)
	}

	accommodationEndpoint := fmt.Sprintf("%s", server.config.AccommodationServiceAddress)
	log.Println(accommodationEndpoint)
	err = accommodation.RegisterAccommodationServiceHandlerFromEndpoint(context.TODO(), server.mux, accommodationEndpoint, opts)
	if err != nil {
		panic(err)
	}

	reservationEndpoint := fmt.Sprintf("%s", server.config.ReservationServiceAddress)
	err = reservation.RegisterReservationServiceHandlerFromEndpoint(context.TODO(), server.mux, reservationEndpoint, opts)
	if err != nil {
		panic(err)
	}
}

func (server *Server) Start() {
	r := mux.NewRouter()

	// Add CORS headers
	r.Use(func(next http.Handler) http.Handler {
		return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
			w.Header().Set("Access-Control-Allow-Origin", "*") // Set the Access-Control-Allow-Origin header to "*"
			w.Header().Set("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS")
			w.Header().Set("Access-Control-Allow-Headers", "Content-Type, Authorization")

			if r.Method == http.MethodOptions {
				w.WriteHeader(http.StatusOK)
				return
			}

			next.ServeHTTP(w, r)
		})
	})
	instrumentation := muxprom.NewDefaultInstrumentation()
	r.Use(instrumentation.Middleware)

	r.PathPrefix("/").Handler(server.mux) // Use the gRPC server's ServeMux

	log.Fatal(http.ListenAndServe(fmt.Sprintf("%s", server.config.Address), r))
}
