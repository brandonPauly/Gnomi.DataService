using System;
using Microsoft.AspNetCore.Mvc;
using gnomi.dataService.requests;
using gnomi.dataService.services;
using gnomi.dataService.responses;

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
            if (data.email is null || data.passwordHash is null)
            {
                return BadRequest();
            }
            try
            {
                var human = _service.addNewHuman(data);

                var response = new newUserResponse
                {
                    email = human.email,
                    humanId = human.humanId
                };
                
                return Ok(response);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Error saving new user to database.");
            }
        }
    }
}
