using Microsoft.AspNetCore.Mvc;
using gnomi.dataService.requests;
using gnomi.dataService.services;
using System.Threading.Tasks;

namespace gnomi.dataService.controllers
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
        public async Task<IActionResult> Post([FromBody] newUserRequest data)
        {
            if (! await _service.isHumanNew(data.email))
            {
                return Conflict("human already exists");
            }
            else
            {
                return Ok(await _service.addNewHuman(data));
            }
        }
    }
}
