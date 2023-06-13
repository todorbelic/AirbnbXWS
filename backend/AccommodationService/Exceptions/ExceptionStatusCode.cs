using Grpc.Core;

namespace AccommodationService.Exceptions
{
    public class ExceptionStatusCode
    {
        private static Dictionary<Type, StatusCode> exceptionStatusCodes = new Dictionary<Type, StatusCode>
        {
            {typeof(Exception), StatusCode.Internal },
            {typeof(AccommodationNotFoundException), StatusCode.NotFound },
        };

        public static StatusCode GetExceptionStatusCode(Exception e)
        {
            bool exceptionFound = exceptionStatusCodes.TryGetValue(e.GetType(), out var statusCode);
            return exceptionFound ? statusCode : StatusCode.Internal;


        }
    }
    

}
