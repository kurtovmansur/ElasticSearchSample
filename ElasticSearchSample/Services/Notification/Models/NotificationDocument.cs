using System;

namespace ElasticSearchSample.Services.Notification.Models
{
    public class NotificationDocument
    {

        public DateTime? CreatedTime { get; set; }
        public NotificationDto Notification { get; set; }
        public Guid NotificationId { get; set; }
        public NotificationDocument(NotificationDto notification)
        {
            Notification = notification;
            Notification.CreatedTime = DateTime.UtcNow;
            CreatedTime = DateTime.UtcNow;
            NotificationId = Guid.NewGuid();
        }

    }
}
