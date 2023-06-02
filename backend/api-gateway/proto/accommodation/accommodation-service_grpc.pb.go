// Code generated by protoc-gen-go-grpc. DO NOT EDIT.
// versions:
// - protoc-gen-go-grpc v1.3.0
// - protoc             v3.19.1
// source: accommodation/accommodation-service.proto

package accommodation

import (
	context "context"
	grpc "google.golang.org/grpc"
	codes "google.golang.org/grpc/codes"
	status "google.golang.org/grpc/status"
)

// This is a compile-time assertion to ensure that this generated file
// is compatible with the grpc package it is being compiled against.
// Requires gRPC-Go v1.32.0 or later.
const _ = grpc.SupportPackageIsVersion7

const (
	AccommodationService_DeleteAllForHost_FullMethodName     = "/AccommodationService/DeleteAllForHost"
	AccommodationService_CreateAccommodation_FullMethodName  = "/AccommodationService/CreateAccommodation"
	AccommodationService_ModifyAccommodation_FullMethodName  = "/AccommodationService/ModifyAccommodation"
	AccommodationService_SearchAcommodations_FullMethodName  = "/AccommodationService/SearchAcommodations"
	AccommodationService_GetAccommodation_FullMethodName     = "/AccommodationService/GetAccommodation"
	AccommodationService_SetReservationOption_FullMethodName = "/AccommodationService/SetReservationOption"
	AccommodationService_GetAllAccommodations_FullMethodName = "/AccommodationService/GetAllAccommodations"
)

// AccommodationServiceClient is the client API for AccommodationService service.
//
// For semantics around ctx use and closing/ending streaming RPCs, please refer to https://pkg.go.dev/google.golang.org/grpc/?tab=doc#ClientConn.NewStream.
type AccommodationServiceClient interface {
	DeleteAllForHost(ctx context.Context, in *DeleteAllForHostRequest, opts ...grpc.CallOption) (*DeleteAllForHostResponse, error)
	CreateAccommodation(ctx context.Context, in *CreateAccommodationRequest, opts ...grpc.CallOption) (*CreateAccommodationResponse, error)
	ModifyAccommodation(ctx context.Context, in *ModifyAccommodationRequest, opts ...grpc.CallOption) (*ModifyAccommodationResponse, error)
	SearchAcommodations(ctx context.Context, in *SearchAcommodationsRequest, opts ...grpc.CallOption) (*SearchAcommodationsResponse, error)
	GetAccommodation(ctx context.Context, in *GetAccommodationRequest, opts ...grpc.CallOption) (*GetAccommodationResponse, error)
	SetReservationOption(ctx context.Context, in *SetReservationOptionRequest, opts ...grpc.CallOption) (*SetReservationOptionResponse, error)
	GetAllAccommodations(ctx context.Context, in *GetAllRequest, opts ...grpc.CallOption) (*GetAllResponse, error)
}

type accommodationServiceClient struct {
	cc grpc.ClientConnInterface
}

func NewAccommodationServiceClient(cc grpc.ClientConnInterface) AccommodationServiceClient {
	return &accommodationServiceClient{cc}
}

func (c *accommodationServiceClient) DeleteAllForHost(ctx context.Context, in *DeleteAllForHostRequest, opts ...grpc.CallOption) (*DeleteAllForHostResponse, error) {
	out := new(DeleteAllForHostResponse)
	err := c.cc.Invoke(ctx, AccommodationService_DeleteAllForHost_FullMethodName, in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *accommodationServiceClient) CreateAccommodation(ctx context.Context, in *CreateAccommodationRequest, opts ...grpc.CallOption) (*CreateAccommodationResponse, error) {
	out := new(CreateAccommodationResponse)
	err := c.cc.Invoke(ctx, AccommodationService_CreateAccommodation_FullMethodName, in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *accommodationServiceClient) ModifyAccommodation(ctx context.Context, in *ModifyAccommodationRequest, opts ...grpc.CallOption) (*ModifyAccommodationResponse, error) {
	out := new(ModifyAccommodationResponse)
	err := c.cc.Invoke(ctx, AccommodationService_ModifyAccommodation_FullMethodName, in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *accommodationServiceClient) SearchAcommodations(ctx context.Context, in *SearchAcommodationsRequest, opts ...grpc.CallOption) (*SearchAcommodationsResponse, error) {
	out := new(SearchAcommodationsResponse)
	err := c.cc.Invoke(ctx, AccommodationService_SearchAcommodations_FullMethodName, in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *accommodationServiceClient) GetAccommodation(ctx context.Context, in *GetAccommodationRequest, opts ...grpc.CallOption) (*GetAccommodationResponse, error) {
	out := new(GetAccommodationResponse)
	err := c.cc.Invoke(ctx, AccommodationService_GetAccommodation_FullMethodName, in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *accommodationServiceClient) SetReservationOption(ctx context.Context, in *SetReservationOptionRequest, opts ...grpc.CallOption) (*SetReservationOptionResponse, error) {
	out := new(SetReservationOptionResponse)
	err := c.cc.Invoke(ctx, AccommodationService_SetReservationOption_FullMethodName, in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *accommodationServiceClient) GetAllAccommodations(ctx context.Context, in *GetAllRequest, opts ...grpc.CallOption) (*GetAllResponse, error) {
	out := new(GetAllResponse)
	err := c.cc.Invoke(ctx, AccommodationService_GetAllAccommodations_FullMethodName, in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

// AccommodationServiceServer is the server API for AccommodationService service.
// All implementations must embed UnimplementedAccommodationServiceServer
// for forward compatibility
type AccommodationServiceServer interface {
	DeleteAllForHost(context.Context, *DeleteAllForHostRequest) (*DeleteAllForHostResponse, error)
	CreateAccommodation(context.Context, *CreateAccommodationRequest) (*CreateAccommodationResponse, error)
	ModifyAccommodation(context.Context, *ModifyAccommodationRequest) (*ModifyAccommodationResponse, error)
	SearchAcommodations(context.Context, *SearchAcommodationsRequest) (*SearchAcommodationsResponse, error)
	GetAccommodation(context.Context, *GetAccommodationRequest) (*GetAccommodationResponse, error)
	SetReservationOption(context.Context, *SetReservationOptionRequest) (*SetReservationOptionResponse, error)
	GetAllAccommodations(context.Context, *GetAllRequest) (*GetAllResponse, error)
	mustEmbedUnimplementedAccommodationServiceServer()
}

// UnimplementedAccommodationServiceServer must be embedded to have forward compatible implementations.
type UnimplementedAccommodationServiceServer struct {
}

func (UnimplementedAccommodationServiceServer) DeleteAllForHost(context.Context, *DeleteAllForHostRequest) (*DeleteAllForHostResponse, error) {
	return nil, status.Errorf(codes.Unimplemented, "method DeleteAllForHost not implemented")
}
func (UnimplementedAccommodationServiceServer) CreateAccommodation(context.Context, *CreateAccommodationRequest) (*CreateAccommodationResponse, error) {
	return nil, status.Errorf(codes.Unimplemented, "method CreateAccommodation not implemented")
}
func (UnimplementedAccommodationServiceServer) ModifyAccommodation(context.Context, *ModifyAccommodationRequest) (*ModifyAccommodationResponse, error) {
	return nil, status.Errorf(codes.Unimplemented, "method ModifyAccommodation not implemented")
}
func (UnimplementedAccommodationServiceServer) SearchAcommodations(context.Context, *SearchAcommodationsRequest) (*SearchAcommodationsResponse, error) {
	return nil, status.Errorf(codes.Unimplemented, "method SearchAcommodations not implemented")
}
func (UnimplementedAccommodationServiceServer) GetAccommodation(context.Context, *GetAccommodationRequest) (*GetAccommodationResponse, error) {
	return nil, status.Errorf(codes.Unimplemented, "method GetAccommodation not implemented")
}
func (UnimplementedAccommodationServiceServer) SetReservationOption(context.Context, *SetReservationOptionRequest) (*SetReservationOptionResponse, error) {
	return nil, status.Errorf(codes.Unimplemented, "method SetReservationOption not implemented")
}
func (UnimplementedAccommodationServiceServer) GetAllAccommodations(context.Context, *GetAllRequest) (*GetAllResponse, error) {
	return nil, status.Errorf(codes.Unimplemented, "method GetAllAccommodations not implemented")
}
func (UnimplementedAccommodationServiceServer) mustEmbedUnimplementedAccommodationServiceServer() {}

// UnsafeAccommodationServiceServer may be embedded to opt out of forward compatibility for this service.
// Use of this interface is not recommended, as added methods to AccommodationServiceServer will
// result in compilation errors.
type UnsafeAccommodationServiceServer interface {
	mustEmbedUnimplementedAccommodationServiceServer()
}

func RegisterAccommodationServiceServer(s grpc.ServiceRegistrar, srv AccommodationServiceServer) {
	s.RegisterService(&AccommodationService_ServiceDesc, srv)
}

func _AccommodationService_DeleteAllForHost_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(DeleteAllForHostRequest)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(AccommodationServiceServer).DeleteAllForHost(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: AccommodationService_DeleteAllForHost_FullMethodName,
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(AccommodationServiceServer).DeleteAllForHost(ctx, req.(*DeleteAllForHostRequest))
	}
	return interceptor(ctx, in, info, handler)
}

func _AccommodationService_CreateAccommodation_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(CreateAccommodationRequest)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(AccommodationServiceServer).CreateAccommodation(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: AccommodationService_CreateAccommodation_FullMethodName,
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(AccommodationServiceServer).CreateAccommodation(ctx, req.(*CreateAccommodationRequest))
	}
	return interceptor(ctx, in, info, handler)
}

func _AccommodationService_ModifyAccommodation_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(ModifyAccommodationRequest)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(AccommodationServiceServer).ModifyAccommodation(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: AccommodationService_ModifyAccommodation_FullMethodName,
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(AccommodationServiceServer).ModifyAccommodation(ctx, req.(*ModifyAccommodationRequest))
	}
	return interceptor(ctx, in, info, handler)
}

func _AccommodationService_SearchAcommodations_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(SearchAcommodationsRequest)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(AccommodationServiceServer).SearchAcommodations(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: AccommodationService_SearchAcommodations_FullMethodName,
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(AccommodationServiceServer).SearchAcommodations(ctx, req.(*SearchAcommodationsRequest))
	}
	return interceptor(ctx, in, info, handler)
}

func _AccommodationService_GetAccommodation_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(GetAccommodationRequest)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(AccommodationServiceServer).GetAccommodation(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: AccommodationService_GetAccommodation_FullMethodName,
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(AccommodationServiceServer).GetAccommodation(ctx, req.(*GetAccommodationRequest))
	}
	return interceptor(ctx, in, info, handler)
}

func _AccommodationService_SetReservationOption_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(SetReservationOptionRequest)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(AccommodationServiceServer).SetReservationOption(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: AccommodationService_SetReservationOption_FullMethodName,
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(AccommodationServiceServer).SetReservationOption(ctx, req.(*SetReservationOptionRequest))
	}
	return interceptor(ctx, in, info, handler)
}

func _AccommodationService_GetAllAccommodations_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(GetAllRequest)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(AccommodationServiceServer).GetAllAccommodations(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: AccommodationService_GetAllAccommodations_FullMethodName,
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(AccommodationServiceServer).GetAllAccommodations(ctx, req.(*GetAllRequest))
	}
	return interceptor(ctx, in, info, handler)
}

// AccommodationService_ServiceDesc is the grpc.ServiceDesc for AccommodationService service.
// It's only intended for direct use with grpc.RegisterService,
// and not to be introspected or modified (even as a copy)
var AccommodationService_ServiceDesc = grpc.ServiceDesc{
	ServiceName: "AccommodationService",
	HandlerType: (*AccommodationServiceServer)(nil),
	Methods: []grpc.MethodDesc{
		{
			MethodName: "DeleteAllForHost",
			Handler:    _AccommodationService_DeleteAllForHost_Handler,
		},
		{
			MethodName: "CreateAccommodation",
			Handler:    _AccommodationService_CreateAccommodation_Handler,
		},
		{
			MethodName: "ModifyAccommodation",
			Handler:    _AccommodationService_ModifyAccommodation_Handler,
		},
		{
			MethodName: "SearchAcommodations",
			Handler:    _AccommodationService_SearchAcommodations_Handler,
		},
		{
			MethodName: "GetAccommodation",
			Handler:    _AccommodationService_GetAccommodation_Handler,
		},
		{
			MethodName: "SetReservationOption",
			Handler:    _AccommodationService_SetReservationOption_Handler,
		},
		{
			MethodName: "GetAllAccommodations",
			Handler:    _AccommodationService_GetAllAccommodations_Handler,
		},
	},
	Streams:  []grpc.StreamDesc{},
	Metadata: "accommodation/accommodation-service.proto",
}