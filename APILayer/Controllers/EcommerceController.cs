using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using BusinessLayer;


namespace APILayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EcommerceController : ControllerBase
    {
        private readonly IBus _bus;

        public EcommerceController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginAsync(LoginDto request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            User? u = await this._bus.LoginAsync(request);

            if(u == null)
            {
                return Unauthorized("Wrong username or password");
            }

            return Ok(u);
        }
    }
}