using ElasticSearchSample.Services;
using ElasticSearchSample.Services.Notification;
using ElasticSearchSample.Services.Notification.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchSample
{
    class Program
    {
        private static string url = "http://localhost:9200";
        private static ElasticClient client = new ElasticClient(new Uri(url));
        async static Task Main(string[] args)
        {
            //await Sample2();
            //for(var x=0; x<50;x++)
            // await AddNotification();
            await AddNotification();
            await GetAllNotifications();

        }

        public void Sample1()
        {
            var context = new ElasticSearchContext();
            var elasticSearchService = new PersonService(context);

            //var person = new Person()
            //{
            //    Id = 2,
            //    Name = "First person",
            //    Email = "Email1"
            //};
            //var response = context.Insert(person, "person_data");

            var persons = elasticSearchService.Search(s => s.From(0).Size(10).Query(q => q.Match(m => m.Field(f => f.Name.StartsWith("First")))));

            /*
            var docs = _eventService.Search(s => s
            .From(filter.Size * (filter.Page - 1))
            .Size(filter.Size)
            .Query(q =>
                (
                    q.Term(t => t.Field(f => f.EventType).Value(filter.EventType)) &&
                    q.DateRange(d => d.Field(f => f.Date)
                        .GreaterThanOrEquals(filter.BeginDate.ToUtcKind())
                        .LessThanOrEquals(filter.EndDate.ToUtcKind()))
                )
                &&
                (
                    q.Terms(t => t.Field(f => f.InfokioskOwnerId).Terms(filter.Owners)) ||
                    q.Terms(t => t.Field(f => f.InfokioskGroupId).Terms(filter.Groups)) ||
                    q.Terms(t => t.Field(f => f.InfokioskId).Terms(filter.Kiosks))
                )
            ));
             */
        }
        public async static Task Sample2()
        {
            var _elasticSearchService = new ElasticSearchService<Person>(client);
            var persons = new List<Person>();
            for (var i = 0; i < 10; i++) { persons.Add(new Person() { Id = i + 1, Name = $"Person{i + 1}", Email = $"email{i}.test.uz" }); }
            await _elasticSearchService.AddBulkAsync(persons);

        }

        public async static Task<List<Person>> GetAllPersons()
        {
            var _elasticSearchService = new ElasticSearchService<Person>(client);
            var search = new SearchDescriptor<Person>();
            var searchResponse = await _elasticSearchService.QueryAsync(search.Query(q => (
            q.Term(t => t.Field(r => r.Name.StartsWith("Per")
            )))));
            var response = searchResponse.Hits.Select(m => (Person)m.Source).ToList();
            
            return await _elasticSearchService.GetAllAsync();
        }

        public async static Task GetAllNotifications()
        {
            var _elasticSearchService = new ElasticSearchService<NotificationDocument>(client);
            var notificationService = new NotificationService(_elasticSearchService);
            var result1 = await notificationService.GetNotificationsAsync(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(1));
            var result2 = await notificationService.GetNotificationsByOrgAsync(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(1), 11);
        }

        public async static Task AddNotification()
        {
            var _elasticSearchService = new ElasticSearchService<NotificationDocument>(client);

            //var not = await _elasticSearchService.DeleteAll();
            var notificationService = new NotificationService(_elasticSearchService);
            await notificationService.Push(new NotificationDto()
            {
                Message="Yangi test2",
                Title = "Title2",
                Type = NotificationType.Info,
                Recivers = new List<NotificationReciver>()
                {
                    new NotificationReciver()
                    {
                        ReciverId = 10,
                        ReciverType=ReciverType.Employee,
                        Status = ViewStatus.NotViewed
                    },
                    new NotificationReciver()
                    {
                        ReciverId = 11,
                        ReciverType=ReciverType.Organization,
                        Status = ViewStatus.NotViewed
                    }
                }
            });

            var result = await notificationService.GetNotificationsAsync(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(1));
        }
    }
}
