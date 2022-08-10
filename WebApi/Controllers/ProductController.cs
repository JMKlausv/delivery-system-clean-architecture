using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.UpdateProduct;
using Application.Products.Queries.GetFilteredProduct;
using Application.Products.Queries.GetProducts;
using Application.Products.Queries.GetSingleProduct;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ApiControllerBase
    {
       
        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IEnumerable<Product>> GetFiltered([FromQuery] string? categoryId)
        {
            if (categoryId == null)
            {
                return await Mediator.Send(new GetProductsQuery());
            }
            var query = new GetFilteredProductQuery
            {
                CategoryId = categoryId,
            };
            return await Mediator.Send(query);
        }



        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<Product> Get(int id)
        {
            var query = new GetSingleProductQuery
            {
                Id = id
            };
            return await Mediator.Send(query);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product product)
        {
            var command = new UpdateProductCommand
            {
                Product = product
            };

            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProductCommand
            {
                Id = id
            };
            return Ok(await Mediator.Send(command));
        }
    }
}
