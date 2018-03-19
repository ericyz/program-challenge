using System.Net.Http;

namespace LovelyCats.Directory.Infrastructures.HttpClientFactory {
    public interface IHttpClientFactory
    {
        HttpClient GetOrCreate();
    }
}