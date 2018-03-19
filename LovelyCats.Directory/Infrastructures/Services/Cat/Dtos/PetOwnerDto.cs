using System.Collections.Generic;

namespace LovelyCats.Directory.Infrastructures.Services.Cat.Dtos {
    public class PetOwnerDto
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }

        public IEnumerable<PetDto> Pets { get; set; }

        public class PetDto
        {
            public string Name { get; set; }
            public string Type { get; set; }
        }
    }
}