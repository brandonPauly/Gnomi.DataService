using gnomi.common.utility.general;
using gnomi.dataService.entities;
using gnomi.dataService.entities.keys;
using gnomi.dataService.requests;
using gnomi.dataService.responses;
using gnomi.repositories;
using System.Threading.Tasks;

namespace gnomi.dataService.services
{
    public class humanService : iHumanService
    {
        private iHumanRepository<long, human<long>> _humanRepository;
        private iVerificationRepository<verificationKey, verification<verificationKey>> _verificationKeyRepository;
        private iRandomStringGenerator _randomStringGenerator;
        private readonly byte numberOfGuids = 3;

        public humanService(iHumanRepository<long, human<long>> repository, iVerificationRepository<verificationKey, verification<verificationKey>> verificationKeyRepository, 
            iRandomStringGenerator randomStringGenerator)
        {
            _humanRepository = repository;
            _randomStringGenerator = randomStringGenerator;
            _verificationKeyRepository = verificationKeyRepository;
        }

        public async Task<newUserResponse> addNewHuman(newUserRequest userRequest)
        {
            var newHuman = new human<long>()
            {
                email = userRequest.email,
                password = userRequest.passwordHash
            };

            newHuman = await _humanRepository.addNewHuman(newHuman);
            newHuman.password = null;

            var verificationKey = getUniqueVerificationKey();

            await _verificationKeyRepository.linkVerification(newHuman.humanId, verificationKey);

            var newUserResponse = new newUserResponse
            {
                email = newHuman.email,
                verificationCode = verificationKey
            };

            return newUserResponse;
        }

        public async Task<bool> isHumanNew(string email)
        {
            return await _humanRepository.isHumanNew(email);
        }

        private string getUniqueVerificationKey()
        {
            return _randomStringGenerator.generateGuidString(numberOfGuids);
        }
    }
}
