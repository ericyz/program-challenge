using System.Collections.Generic;

namespace LovelyCats.Directory.Features.Cats.RequestHandlers {
    public class GetCatResultOutputModel
    {
        public GetCatResultOutputModel()
        {
            FeatureGroups = new Dictionary<string, IEnumerable<string>>();
        }
        public bool Success { get; set; }
        public IDictionary<string, IEnumerable<string>> FeatureGroups { get; set; }
    }
}