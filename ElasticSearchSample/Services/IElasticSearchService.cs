using Nest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElasticSearchSample.Services
{
    public interface IElasticSearchService<T> where T : class
    {
        IElasticSearchService<T> Index(string indexName);
        Task<BulkResponse> AddOrUpdateBulkAsync(IEnumerable<T> documents);
        Task<T> AddOrUpdateAsync(T document);
        Task<BulkResponse> AddBulkAsync(IList<T> documents);
        Task<GetResponse<T>> GetAsync(string key);
        Task<ISearchResponse<T>> QueryAsync(SearchDescriptor<T> sd);
        Task<bool> RemoveAsync(string key);
        Task<DeleteByQueryResponse> BulkRemoveAsync(IDeleteByQueryRequest<T> queryReq);
    }
}
