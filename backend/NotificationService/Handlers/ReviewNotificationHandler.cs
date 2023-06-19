using Grpc.Core;


namespace NotificationService.Handlers
{
    public class ReviewNotificationHandler:ReviewNotification.ReviewNotificationBase
    {
        public override Task<RateHostNotificationResponse> RateHost(RateHostNotificationRequest request, ServerCallContext context)
        {
            Console.WriteLine("Ushaooo");
            return Task.FromResult(new RateHostNotificationResponse());
        }
    }
}
