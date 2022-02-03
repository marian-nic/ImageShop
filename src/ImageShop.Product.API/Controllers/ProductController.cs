using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using ImageShop.Product.Dtos.ProductDtos;
using System.Collections.Generic;
using ImageShop.Product.Application.Queries;

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
    }
}
