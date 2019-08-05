using gnomi.dataService.entities;
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
            var newHuman = new human<long>()
            {
                email = userRequest.email,
                password = userRequest.passwordHash
            };

            newHuman = _repository.addNewHuman(newHuman);
            newHuman.password = null;

            return newHuman;
        }
    }
}
