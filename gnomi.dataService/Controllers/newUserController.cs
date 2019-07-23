using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gnomi.dataService.requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gnomi.dataService.Controllers
{
    [Route("api/newUser")]
    [ApiController]
    public class newUserController : ControllerBase
    {
        // POST: api/newUser
        [HttpPost]
        public void Post([FromBody] newUserRequest data)
        {
            var check = data;
        }
    }
}
