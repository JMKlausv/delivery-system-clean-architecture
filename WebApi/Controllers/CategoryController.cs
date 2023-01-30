using Application.Categories.Commands.CreateCategpry;
using Application.Categories.Commands.DeleteCategory;
using Application.Categories.Commands.PatchCategory;
using Application.Categories.Commands.UpdateCategory;
using Application.Categories.Queries.GetCategories;
using Application.Categories.Queries.GetSingleCategory;
using Application.Common.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
  
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CategoryController : ApiControllerBase
    {
        // GET: api/<CategoryController>
        [HttpGet]
       
        public async Task<IEnumerable<Category>> GetAsync()
        {
            return await Mediator.Send(new GetCategoriesQuery());
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<Category> Get(int id)
        {
 
            return await Mediator.Send(new GetSingleCategoryQuery(id));
        }

        // POST api/<CategoryController>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        public async Task<IActionResult> Post([FromBody] CreateCategoryCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (ValidationException ex)
            {

                return BadRequest(ex.Message);
            }
         

        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
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
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        public async  Task<int> Delete(int id)
        {
            var cmd = new DeleteCategoryCommand
            {
                Id = id
            };
           return  await Mediator.Send(cmd);
        }

        //PATCH api/<CategoryController>/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<Category> resource)
        {
            var command = new PatchCategoryCommand
            {
                Id = id,
                categoryPatch = resource
            };
            return Ok(await Mediator.Send(command));
        }
    }
}
