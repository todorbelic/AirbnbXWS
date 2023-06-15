package main

import (
	"airbnb/api-gateway/startup"
	"airbnb/api-gateway/startup/config"
	"os"
	"os/signal"
	"syscall"

	"github.com/joho/godotenv"
	"github.com/prometheus/common/log"
)

func main() {

	sigs := make(chan os.Signal, 1)
	done := make(chan bool, 1)
	signal.Notify(sigs, os.Interrupt, syscall.SIGINT, syscall.SIGTERM)

	go func() {
		<-sigs
		log.Info("API Gateway stopped")
		done <- true
		os.Exit(0)
	}()
	err := godotenv.Load(".env")
	if err != nil {
		log.Fatal(err)
	}
	config := config.GetConfig()
	server := startup.NewServer(config)
	log.Info("API Gateway started")
	server.Start()
	<-done
}
