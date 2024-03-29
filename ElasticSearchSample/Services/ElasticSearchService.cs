﻿using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchSample.Services
{
    public class ElasticSearchService<T> : IElasticSearchService<T> where T : class
    {
        private string IndexName { get; set; }
        private readonly IElasticClient _client;

        public ElasticSearchService(IElasticClient client)
        {
            _client = client;
            IndexName = typeof(T).Name.ToLower() + "s";
        }

        public IElasticSearchService<T> Index(string indexName)
        {
            IndexName = indexName;
            return this;
        }

        public async Task<BulkResponse> AddOrUpdateBulkAsync(IEnumerable<T> documents)
        {
            var indexResponse = await _client.BulkAsync(b => b
                .Index(IndexName)
                .UpdateMany(documents, (ud, d) => ud.Doc(d).DocAsUpsert())
            );
            return indexResponse;
        }

        public async Task<T> AddOrUpdateAsync(T document)
        {
            var indexResponse =
                await _client.IndexAsync(document, idx => idx.Index(IndexName));
            if (!indexResponse.IsValid)
            {
                throw new Exception(indexResponse.DebugInformation);
            }

            return document;
        }

        public async Task<BulkResponse> AddBulkAsync(IList<T> documents)
        {
            var resp = await _client.BulkAsync(b => b
                .Index(IndexName)
                .IndexMany(documents)
            );
            return resp;
        }

        public async Task<GetResponse<T>> GetAsync(string key)
        {
            return await _client.GetAsync<T>(key, g => g.Index(IndexName));
        }

        public async Task<List<T>> GetAllAsync()
        {
            var searchResponse = await _client.SearchAsync<T>(s => s.Index(IndexName).Skip(0).Size(0).Query(q => q.MatchAll()));
            return searchResponse.IsValid ? searchResponse.Documents.ToList() : default;
        }

        public async Task<ISearchResponse<T>> QueryAsync(SearchDescriptor<T> sd)
        {
            sd.Index(IndexName);
            var searchResponse = await _client.SearchAsync<T>(sd);
            return searchResponse;
        }

        public async Task<bool> RemoveAsync(string key)
        {
            var response = await _client.DeleteAsync<T>(key, d => d.Index(IndexName));
            return response.IsValid;
        }

        public async Task<DeleteByQueryResponse> BulkRemoveAsync(IDeleteByQueryRequest<T> queryReq)
        {
            var response = await _client.DeleteByQueryAsync(queryReq);
            return response;
        }
        public async Task<DeleteByQueryResponse> DeleteAll()
        {
            var response = await _client.DeleteByQueryAsync<T>(del => 
            del.Index(IndexName)
               .Query(q => q.QueryString(qs => qs.Query("*"))));
            
            return response;
        }
    }
}
