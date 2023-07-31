using Grpc.Core;
using ReviewService.Exceptions;

namespace ReviewService.Exceptions
{
    public class ExceptionStatusCode
    {
        private static Dictionary<Type, StatusCode> exceptionStatusCodes = new Dictionary<Type, StatusCode>
        {
            {typeof(Exception), StatusCode.Internal },
            {typeof(CannotRateException), StatusCode.AlreadyExists },
        };

        public static StatusCode GetExceptionStatusCode(Exception e)
        {
            bool exceptionFound = exceptionStatusCodes.TryGetValue(e.GetType(), out var statusCode);
            return exceptionFound ? statusCode : StatusCode.Internal;
        }
    }
    

}
