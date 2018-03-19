using System.Collections.Generic;
using System.Threading.Tasks;
using LovelyCats.Directory.Infrastructures.Services.Cat.Dtos;

namespace LovelyCats.Directory.Infrastructures.Services.Cat {
    public interface IPetService
    {
        Task<IList<PetOwnerDto>> GetAllAsync();
    }
}