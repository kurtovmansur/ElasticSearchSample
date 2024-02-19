namespace ElasticSearchSample.Services.Notification.Models
{
    public class NotificationReciver
    {
        public int ReciverId { get; set; }
        public ReciverType ReciverType { get; set; }
        public ViewStatus Status { get; set; } = ViewStatus.NotViewed;
    }
}
