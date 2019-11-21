using Microsoft.AspNetCore.Mvc;
using gnomi.dataService.requests;
using gnomi.dataService.services;
using System.Threading.Tasks;

namespace gnomi.dataService.controllers
{
    [Route("api/newHuman")]
    [ApiController]
    public class newHumanController : ControllerBase
    {
        private iHumanService _service;

        public newHumanController(iHumanService service)
        {
            _service = service;
        }

        // POST: api/newHuman
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] newHumanRequest data)
        {
            if (! await _service.isHumanNew(data.email))
            {
                return Conflict("existing human");
            }
            else
            {
                return Ok(await _service.addNewHuman(data));
            }
        }
    }
}
