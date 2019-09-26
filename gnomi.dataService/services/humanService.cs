using gnomi.common.utility.general;
using gnomi.dataService.entities;
using gnomi.dataService.requests;
using gnomi.dataService.responses;
using gnomi.repositories;

namespace gnomi.dataService.services
{
    public class humanService : iHumanService
    {
        private iHumanRepository<long, human<long>> _repository;
        private iRandomStringGenerator _randomStringGenerator;
        private readonly byte minimumVerificationLength = 12;
        private readonly byte maximumVerificationLength = 36;

        public humanService(iHumanRepository<long, human<long>> repository, iRandomStringGenerator randomStringGenerator)
        {
            _repository = repository;
            _randomStringGenerator = randomStringGenerator;
        }

        public newUserResponse addNewHuman(newUserRequest userRequest)
        {
            var newHuman = new human<long>()
            {
                email = userRequest.email,
                password = userRequest.passwordHash
            };

            newHuman = _repository.addNewHuman(newHuman);
            newHuman.password = null;

            var verificationCode = _randomStringGenerator.generateRandomSequence(minimumVerificationLength, maximumVerificationLength);

            _repository.linkVerification(newHuman.humanId, verificationCode);

            var newUserResponse = new newUserResponse
            {
                email = newHuman.email,
                humanId = newHuman.humanId,
                verificationCode = verificationCode
            };

            return newUserResponse;
        }

        public void submitVerificationCode(long humanId, string verificationCode)
        {
            _repository.linkVerification(humanId, verificationCode);
        }
    }
}
