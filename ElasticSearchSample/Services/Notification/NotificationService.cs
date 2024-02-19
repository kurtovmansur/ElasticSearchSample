using ElasticSearchSample.Services.Notification.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchSample.Services.Notification
{
    public class NotificationService
    {

        private readonly IElasticSearchService<NotificationDocument> _elasticsearchService;

        public NotificationService(IElasticSearchService<NotificationDocument> elasticsearchService)
        {
            _elasticsearchService = elasticsearchService;
        }

        public async Task Push(NotificationDto notification)
        {
            var notificationDoc = new NotificationDocument(notification);
            var apiresponse = await _elasticsearchService.AddOrUpdateAsync(notificationDoc);
        }
        public async Task<List<NotificationDto>> GetNotificationsAsync(DateTime startDate, DateTime endDate)
        {
            var search = new SearchDescriptor<NotificationDocument>();
            var query = search.Query(q =>
            q.DateRange(d => d.Field(f => f.CreatedTime).GreaterThan(startDate.ToUniversalTime()).LessThan(endDate.ToUniversalTime())));
            try
            {
                var docs = await _elasticsearchService.QueryAsync(query.Skip(0).Take(10000));
                var result = docs.Hits.Select(s => new NotificationDto(s.Source)).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<NotificationDto>> GetNotificationsByOrgAsync(DateTime startDate, DateTime endDate, int orgId)
        {
            var searchByNotification = new SearchDescriptor<NotificationDocument>();
            var query = searchByNotification.Query(q =>
                                            q.DateRange(d => d
                                            .Field(f => f.CreatedTime)
                                            .GreaterThan(startDate.ToUniversalTime()).LessThan(endDate.ToUniversalTime())) &&
                                            q.Term(t => t.Field(a => a.Notification.Recivers[0].ReciverId).Value(orgId))).Take(0).Size(10000);
            try
            {

                var docs = await _elasticsearchService.QueryAsync(query);
                var result = docs.Hits.Select(s => new NotificationDto(s.Source)).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
