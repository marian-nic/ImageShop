using ImageShop.Data.Abstraction;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace ImageShop.Data.Cosmos
{
    public class CosmosRepository<T> : ICosmosRepository<T> where T : CosmosResource
    {
        private readonly string _databaseId;
        private readonly string _containerId;
        private CosmosClient _cosmosClient;

        public CosmosRepository(string endpoint, string key, string databaseId, string containerId)
        {
            _cosmosClient = new CosmosClient(endpoint, key, new CosmosClientOptions() { ApplicationName = Assembly.GetExecutingAssembly().GetName().Name });
            _databaseId = databaseId;
            _containerId = containerId;
        }

        public Container CosmosContainer
        {
            get
            {
                return _cosmosClient.GetContainer(_databaseId, _containerId);
            }
        }

        public async Task InitializeDatabaseAndContainer()
        {
            //create database if not exists
            var database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseId);

            //create container if not exists
            await database.Database.CreateContainerIfNotExistsAsync(_containerId, "/partitionKey");
        }

        public async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> filterExpression, string partitionKey = null)
        {
            List<T> result = new List<T>();

            var queryRequestOptions = string.IsNullOrWhiteSpace(partitionKey) ? new QueryRequestOptions() : new QueryRequestOptions()
            {
                PartitionKey = new PartitionKey(partitionKey)
            };

            var querable = CosmosContainer.GetItemLinqQueryable<T>(requestOptions: queryRequestOptions)
                .Where(filterExpression);

            if (!string.IsNullOrWhiteSpace(partitionKey))
                querable = querable.Where(x => x.PartitionKey == partitionKey);

            using var feedIterator = querable.ToFeedIterator();

            while (feedIterator.HasMoreResults)
            {
                FeedResponse<T> feedResponse = await feedIterator.ReadNextAsync();

                if (feedResponse != null && feedResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if (feedResponse.Resource.Any())
                    {
                        result.AddRange(feedResponse.Resource);
                    }
                }
            }
            return result;
        }

        public async Task<T> CreateAsync(T item, bool returnCreatedItem = false)
        {
            var partitionKey = new PartitionKey(item.PartitionKey);
            var itemResponse = await CosmosContainer.CreateItemAsync<T>(item, partitionKey,
                new ItemRequestOptions()
                {
                    EnableContentResponseOnWrite = returnCreatedItem
                });

            if(itemResponse != null && itemResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return itemResponse.Resource;
            }

            return default(T);
        }
    }
}
