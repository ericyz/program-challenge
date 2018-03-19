using System;
using System.Net.Http;

namespace LovelyCats.Directory.Infrastructures.HttpClientFactory {
    public class HttpClientFactory : IHttpClientFactory
    {
        private readonly Lazy<HttpClient> _httpClientLazy = new Lazy<HttpClient>(Create);
        public HttpClient GetOrCreate()
        {
            return _httpClientLazy.Value;
        }

        private static HttpClient Create()
        {
            return new HttpClient();
        }
    }
}