using Nest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElasticSearchSample.Services
{
    public interface IElasticSearchService<T> where T : class
    {
        /// <summary>
        /// Index yaratish uchun ishlatiladi,
        /// barcha documentlar shu indexda saqlanishi mumkin
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        IElasticSearchService<T> Index(string indexName);
        
        /// <summary>
        /// Bir yoki bir necta dokumentni Insert yoki update qilish uchun ishlatiladi
        /// </summary>
        /// <param name="documents"></param>
        /// <returns></returns>
        Task<BulkResponse> AddOrUpdateBulkAsync(IEnumerable<T> documents);
        
        /// <summary>
        /// 1 ta dona dokumentni insert yoki update qilish uchun ishlatiladi
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        Task<T> AddOrUpdateAsync(T document);
        
        /// <summary>
        /// Bir nechta dokumentni insert qilish uchun ishlatiladi
        /// </summary>
        /// <param name="documents"></param>
        /// <returns></returns>
        Task<BulkResponse> AddBulkAsync(IList<T> documents);
        
        /// <summary>
        /// Berilgan key bo'yicha filterlash uchun ishlatiladi
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<GetResponse<T>> GetAsync(string key);
        
        /// <summary>
        /// So'rovni amalga oshirish uchun ishlatiladi
        /// </summary>
        /// <param name="sd"></param>
        /// <returns></returns>
        Task<ISearchResponse<T>> QueryAsync(SearchDescriptor<T> sd);
        Task<bool> RemoveAsync(string key);
        
        /// <summary>
        /// So'rov bo'yicha bir yoki bir nechta ma'lumotni o'chirish
        /// </summary>
        /// <param name="queryReq"></param>
        /// <returns></returns>
        Task<DeleteByQueryResponse> BulkRemoveAsync(IDeleteByQueryRequest<T> queryReq);
    }
}
