using Grpc.Core;

namespace ReservationService.Exceptions
{
    public static class ExceptionStatusCode
    {
        private static Dictionary<Type, StatusCode> exceptionStatusCodes = new Dictionary<Type, StatusCode>
        {
            {typeof(Exception), StatusCode.Internal}
        };

        public static StatusCode GetExceptionStatusCode(Exception ex)
        {
            bool exceptionFound = exceptionStatusCodes.TryGetValue(ex.GetType(), out var statusCode);
            return exceptionFound ? statusCode : StatusCode.Internal;
        }
    }
}
