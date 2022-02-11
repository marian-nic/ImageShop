using Newtonsoft.Json;
using System;

namespace ImageShop.Data.Abstraction
{
    public abstract class CosmosResource
    {
        [JsonProperty(PropertyName ="id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "partitionKey")]
        public string PartitionKey { get; set; }
    }
}
