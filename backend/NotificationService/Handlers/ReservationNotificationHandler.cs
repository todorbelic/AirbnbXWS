using Grpc.Core;

namespace NotificationService.Handlers
{
    public class ReservationNotificationHandler:ReservationNotification.ReservationNotificationBase
    {
        public override Task<ReservationAcceptedResponse> ReservationAccepted(ReservationAcceptedRequest request, ServerCallContext context)
        {
            return base.ReservationAccepted(request, context);
        }
    }
}
