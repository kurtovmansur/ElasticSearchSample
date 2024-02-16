using System;
using System.Collections.Generic;

namespace ElasticSearchSample.Services.Models
{
    public class NotificationDto
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime? CreatedTime { get; set; }
        public NotificationType Type { get; set; }
        public List<NotificationReciver> Recivers { get; set; }
    }
}
 