using Application.Categories.Commands.CreateCategpry;
using Application.Categories.Commands.DeleteCategory;
using Application.Categories.Commands.UpdateCategory;
using Application.Categories.Queries.GetCategories;
using Application.Categories.Queries.GetSingleCategory;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ApiControllerBase
    {
        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<IEnumerable<Category>> GetAsync()
        {
            return await Mediator.Send(new GetViechlesQuery());
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<Category> Get(int id)
        {

            return await Mediator.Send(new GetSingleCategoryQuery(id));
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryCommand command)
        {
          return Ok( await  Mediator.Send(command));

        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Category category)
        {
            var cmd = new UpdateCategoryCommand
            {
                Id = id,
                Category = category
            };
          return  Ok( await Mediator.Send(cmd));
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async  Task<int> Delete(int id)
        {
            var cmd = new DeleteCategoryCommand
            {
                Id = id
            };
           return  await Mediator.Send(cmd);
        }
    }
}
