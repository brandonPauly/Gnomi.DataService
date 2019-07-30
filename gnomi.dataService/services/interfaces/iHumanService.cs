using gnomi.dataService.entities;
using gnomi.dataService.requests;

namespace gnomi.dataService.services
{
    public interface iHumanService
    {
        human<long> addNewHuman(newUserRequest userRequest);
    }
}
