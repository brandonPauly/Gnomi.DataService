using gnomi.dataService.requests;
using gnomi.dataService.services;
using Microsoft.AspNetCore.Mvc;

namespace gnomi.dataService.Controllers
{
    [Route("api/newUser")]
    [ApiController]
    public class newUserController : ControllerBase
    {
        private iHumanService _service;

        public newUserController(iHumanService service)
        {
            _service = service;
        }
        // POST: api/newUser
        [HttpPost]
        public void Post([FromBody] newUserRequest data)
        {
            _service.addNewHuman(data);
        }
    }
}
