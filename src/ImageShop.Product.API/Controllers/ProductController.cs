using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using ImageShop.Product.Dtos.ProductDtos;
using System.Collections.Generic;
using ImageShop.Product.Application.Queries;
using ImageShop.Product.Application.Commands;
using ImageShop.Product.Dtos.CommonDtos;

namespace ImageShop.Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Get a filtered list of products
        /// </summary>
        /// <param name="title">Search for a title</param>
        /// <param name="tag">Search for a tag</param>
        /// <returns>A list of products</returns>
        [ProducesResponseType(typeof(List<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string title, [FromQuery] string tag)
        {
            var query = new GetProductsQuery()
            {
                SearchedTitle = title,
                SearchedTag = tag
            };

            var respose = await _mediator.Send(query);

            if (respose == null || !respose.ProcessedSuccessfully)
                return BadRequest(respose?.ErrorMessage);

            return Ok(respose.Payload);
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="newProductDto"> Information regarding the new product</param>
        /// <returns>Id of the product</returns>
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductDto newProductDto)
        {
            var command = new CreateProductCommand()
            {
                NewProduct = newProductDto,
                Owner = new UserDto()
                {
                    Name = "Authenticated User Name",
                    Email = "test@something.com"
                }
            };

            var response = await _mediator.Send(command);

            if(response == null || !response.ProcessedSuccessfully)
                return BadRequest(response?.ErrorMessage);

            return Ok(response.Payload);
        }
    }
}
