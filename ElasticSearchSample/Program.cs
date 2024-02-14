using ElasticSearchSample.Services;
using Nest;
using System.Security.Policy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElasticSearchSample
{
    class Program
    {
        async static Task Main(string[] args)
        {
            //await Sample2();
            var persons = await GetAllPersons();

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
            var url = "http://localhost:9200";
            var client = new ElasticClient(new Uri(url));
            var _elasticSearchService = new ElasticSearchService<Person>(client);
            var persons = new List<Person>();
            for (var i = 0; i < 10; i++) { persons.Add(new Person() { Id = i + 1, Name = $"Person{i + 1}", Email = $"email{i}.test.uz" }); }
            await _elasticSearchService.AddBulk(persons);

        }

        public async static Task<List<Person>> GetAllPersons()
        {
            var url = "http://localhost:9200";
            var client = new ElasticClient(new Uri(url));
            var _elasticSearchService = new ElasticSearchService<Person>(client);
            return await _elasticSearchService.GetAll();
        }
    }
}
