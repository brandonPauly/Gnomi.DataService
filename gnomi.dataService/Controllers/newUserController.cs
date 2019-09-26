using Microsoft.AspNetCore.Mvc;
using gnomi.dataService.requests;
using gnomi.dataService.services;

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
        public IActionResult Post([FromBody] newUserRequest data)
        {
            var response = _service.addNewHuman(data);
            return Ok(response); 
        }
    }
}
