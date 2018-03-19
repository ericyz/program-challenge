using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LovelyCats.Directory.Infrastructures.Services.Cat;
using LovelyCats.Directory.Infrastructures.Services.Cat.Dtos;
using MediatR;

namespace LovelyCats.Directory.Features.Cats.RequestHandlers
{
    public class GetCatResultRequestHandler : IRequestHandler<GetCatResultRequest, GetCatResultOutputModel>
    {
        private readonly IPetService _petService;

        public GetCatResultRequestHandler(IPetService petService)
        {
            _petService = petService;
        }

        public async Task<GetCatResultOutputModel> Handle(GetCatResultRequest request, CancellationToken cancellationToken)
        {
            var output = new GetCatResultOutputModel();

            var cats = await _petService.GetAllAsync();

            if (cats?.Any() != true)
            {
                return output;
            }

            var genders = cats.Select(s => s.Gender).Distinct();
            foreach (var gender in genders)
            {
                output.FeatureGroups.Add(gender, cats.Where(c => c.Gender == gender && c.Pets?.Any() == true)
                    .SelectMany(s => s.Pets.Where(p => p.Type == "Cat"))
                    .Select(c => c.Name).OrderBy(s => s).ToList());
            }
            output.Success = true;
            return output;

        }
    }
}