using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LovelyCats.Directory.Infrastructures.HttpClientFactory;
using LovelyCats.Directory.Infrastructures.Services.Cat.Dtos;
using LovelyCats.Directory.Infrastructures.Services.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LovelyCats.Directory.Infrastructures.Services.Cat
{
    public class PetService : IPetService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AppSettings _setting;

        public PetService(IHttpClientFactory httpClientFactory, IOptions<AppSettings> appSetting)
        {
            _httpClientFactory = httpClientFactory;
            _setting = appSetting.Value;
        }

        public async Task<IList<PetOwnerDto>> GetAllAsync()
        {
            var httpClient = _httpClientFactory.GetOrCreate();
            httpClient.Timeout = TimeSpan.FromSeconds(10);

            var response = await httpClient.GetAsync(_setting.CatEndpoint);

            if (!response.IsSuccessStatusCode || response.Content == null)
                return null;

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                using (var jsonReader = new JsonTextReader(new StreamReader(stream)))
                {
                    var serializer = JsonSerializer.Create();
                    return serializer.Deserialize<IList<PetOwnerDto>>(jsonReader);
                }
            }
        }
    }
}