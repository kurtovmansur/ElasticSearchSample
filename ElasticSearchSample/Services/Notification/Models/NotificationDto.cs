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

        /// <summary>
        /// Xabar sarlovhasi
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Xabar matni
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Yaratilgan vaqti, UniversalTime shaklda
        /// </summary>
        public DateTime CreatedTime { get; set; }
        public NotificationType Type { get; set; }

        /// <summary>
        /// Kim uchun? (Xabarni oluvchilar)
        /// </summary>
        public List<NotificationReciver> Recivers { get; set; }
    }
}
