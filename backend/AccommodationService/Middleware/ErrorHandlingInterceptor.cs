using AccommodationService.Exceptions;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace AccommodationService.Middleware
{
    public class ErrorHandlingInterceptor:Interceptor
    {
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                // Call the next middleware or service method
                return await continuation(request, context);
            }
            catch (Exception ex)
            {
                // Handle and process the exception
                var status = new Status(ExceptionStatusCode.GetExceptionStatusCode(ex), ex.Message);
                throw new RpcException(status);
            }
        }
    }
}
