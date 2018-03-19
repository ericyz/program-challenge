using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LovelyCats.Directory.Features.Cats.RequestHandlers;
using LovelyCats.Directory.Infrastructures.Services.Cat;
using LovelyCats.Directory.Infrastructures.Services.Cat.Dtos;
using Shouldly;
using Xunit;

namespace LovelyCats.Directory.Test
{
    public class GetCatResultRequestHandlerTest
    {
        [Theory, ClassData(typeof(JsonTestData<GetCatResultRequestHandlerTestData>))]
        public void GetCatResultRequestHandlerShouldPass(GetCatResultRequestHandlerTestData data)
        {
            var petService = new Moq.Mock<IPetService>();
            petService.Setup(s => s.GetAllAsync()).Returns(Task.FromResult(data.Data));
            var handler = new GetCatResultRequestHandler(petService.Object);
            var result = handler.Handle(new GetCatResultRequest(), CancellationToken.None).Result;

            var maleGroup = result.FeatureGroups.TryGetValue("Male", out var outMaleGroup) ? outMaleGroup.ToList() : null;
            var femaleGroup = result.FeatureGroups.TryGetValue("Female", out var outFemaleGroup) ? outFemaleGroup.ToList() : null;


            (maleGroup?.Count ?? 0).ShouldBe(data.ExpectedResults.MaleCatCount);
            (femaleGroup?.Count ?? 0).ShouldBe(data.ExpectedResults.FemaleCatCount);
            maleGroup?.FirstOrDefault().ShouldBe(data.ExpectedResults.FirstMaleName);
            femaleGroup?.FirstOrDefault().ShouldBe(data.ExpectedResults.FirstFemaleName);
            maleGroup?.LastOrDefault().ShouldBe(data.ExpectedResults.LastMaleName);
            femaleGroup?.LastOrDefault().ShouldBe(data.ExpectedResults.LastFemaleName);
        }
    }

    [Serializable]
    public class GetCatResultRequestHandlerTestData
    {
        public ExpectedResult ExpectedResults { get; set; }
        public IList<PetOwnerDto> Data { get; set; }
    }

    public class ExpectedResult
    {
        public int FemaleCatCount { get; set; }
        public int MaleCatCount { get; set; }
        public string FirstFemaleName { get; set; }
        public string FirstMaleName { get; set; }
        public string LastFemaleName { get; set; }
        public string LastMaleName { get; set; }
    }
}
