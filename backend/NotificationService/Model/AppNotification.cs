using AccommodationService.Enums;
using NotificationService.Attributes;

namespace NotificationService.Model
{
    [BsonCollection("notifications")]

    public class AppNotification:Document
    {
        public AppNotification() { }
        public string UserId { get; set; }
        public NotificationType NotificationType { get; set; }
        public string NotificationText { get; set; }
        public string GuestId { get; set; }
        public string AccomId { get; set; }
        public int Rating { get; set; }

        /*RESERVATION_REQUEST,
        RESERVATION_CANCEL,
        HOST_RATE,
        ACCOM_RATE,
        GOT_FEATURED_HOST,
        LOST_FEATURED_HOST,
        GUEST_ACCEPTED*/

        public void SetNotificationText(string guestName,string accomName)
        {
            switch (NotificationType)
            {
                case NotificationType.RESERVATION_REQUEST: NotificationText = "Your "+accomName+" has a reservation request from "+guestName+".";break;
                case NotificationType.RESERVATION_CANCEL: NotificationText = "Reservation for "+accomName+" has been cancelled by "+guestName+"."; break;
                case NotificationType.HOST_RATE: NotificationText = "A guest has rated you with "+Rating.ToString()+"."; break;
                case NotificationType.ACCOM_RATE: NotificationText = "A guest has rated your "+accomName+" with "+Rating.ToString(); break;
                case NotificationType.GOT_FEATURED_HOST: NotificationText = "Congratulations! You became a featured host."; break;
                case NotificationType.LOST_FEATURED_HOST: NotificationText = "Unfortunately, you've lost your featured host title."; break;
                case NotificationType.GUEST_ACCEPTED: NotificationText = "Your reservation for "+accomName+" has been accepted!"; break;


            }
        }
    }
}
