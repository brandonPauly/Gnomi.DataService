using gnomi.dataService.requests;
using gnomi.dataService.responses;

namespace gnomi.dataService.services
{
    public interface iHumanService
    {
        newUserResponse addNewHuman(newUserRequest userRequest);

        void submitVerificationCode(long humanId, string verificationCode);
    }
}
