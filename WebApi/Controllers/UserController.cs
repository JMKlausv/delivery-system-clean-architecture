using Application.User.Commands.AuthenticateUser;
using Application.User.Commands.CreateUser;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {

        // POST api/<UserController>
        [HttpPost("Register")]
        public async Task<IActionResult> createUser([FromBody] CreateUserCommand command)
        {
            return Ok(await Mediator.Send(command));

        }

        // POST api/<UserController>
        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] AuthenticateUserCommand command)
        {
            return Ok(await Mediator.Send(command));

        }



    }
}
