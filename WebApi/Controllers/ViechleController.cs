using Application.Viechles.Commands.CreateViechle;
using Application.Viechles.Commands.DeleteViechle;
using Application.Viechles.Commands.UpdateViechle;
using Application.Viechles.Queries.GetSingleViechle;
using Application.Viechles.Queries.GetViechles;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ViechleController : ApiControllerBase
    {
        // GET: api/<ViechleController>
        [HttpGet]
        public async Task<IEnumerable<Viechle>> Get()
        {
            return await Mediator.Send(new GetViechlesQuery());
        }

        // GET api/<ViechleController>/5
        [HttpGet("{id}")]
        public async Task<Viechle> Get(int id)
        {
            return await Mediator.Send(new GetSingleViechleQuery(id));
        }
       
       // POST api/<ViechleController>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        public async Task<IActionResult> Post([FromBody] CreateViechleCommand command)
                {
                    return Ok(await Mediator.Send(command));
                }

         // PUT api/<ViechleController>/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        public async Task<IActionResult> Put(int id, [FromBody] Viechle viechle)
        {
           var cmd = new UpdateViechleCommand
           {
             Id = id,
             Viechle = viechle
           };
         return Ok(await Mediator.Send(cmd));
        }

        // DELETE api/<ViechleController>/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
             var cmd = new DeleteViechleCommand
             {
                 Id = id
             };
          return Ok(await Mediator.Send(cmd));
        }
             
    }
}
