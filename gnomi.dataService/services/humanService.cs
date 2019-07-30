using gnomi.dataService.entities;
using gnomi.dataService.metadata;
using gnomi.dataService.requests;
using gnomi.repositories;

namespace gnomi.dataService.services
{
    public class humanService : iHumanService
    {
        private iHumanRepository<long, human<long>> _repository;

        public humanService(iHumanRepository<long, human<long>> repository)
        {
            _repository = repository;
        }

        public human<long> addNewHuman(newUserRequest userRequest)
        {
            var newHuman = new human<long>(new humanMetadata<long>())
            {
                email = userRequest.email,
                password = userRequest.passwordHash
            };

            _repository.add(newHuman);

            return newHuman;
        }
    }
}
