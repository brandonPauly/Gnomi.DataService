using gnomi.dataService.requests;
using gnomi.dataService.responses;
using System.Threading.Tasks;

namespace gnomi.dataService.services
{
    public interface iHumanService
    {
        Task<newUserResponse> addNewHuman(newUserRequest userRequest);
        Task<bool> isHumanNew(string email);
    }
}
