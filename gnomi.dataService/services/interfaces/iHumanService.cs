using gnomi.dataService.requests;
using gnomi.dataService.responses;
using System.Threading.Tasks;

namespace gnomi.dataService.services
{
    public interface iHumanService
    {
        Task<newHumanResponse> addNewHuman(newHumanRequest userRequest);
        Task<bool> isHumanNew(string email);
    }
}
