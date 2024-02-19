using Nest;
using System;
using System.Collections.Generic;

namespace ElasticSearchSample.Services.Notification.Models
{
    public class NotificationDto
    {
        public NotificationDto()
        {

        }

        public NotificationDto(NotificationDocument source)
        {
            Title = source.Notification.Title;
            Message = source.Notification.Message;
            Type = source.Notification.Type;
            Recivers = source.Notification.Recivers;
            CreatedTime = source.Notification.CreatedTime.AddHours(5);
        }

        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreatedTime { get; set; }
        public NotificationType Type { get; set; }
        public List<NotificationReciver> Recivers { get; set; }
    }
}
