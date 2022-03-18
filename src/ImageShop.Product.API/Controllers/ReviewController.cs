using ImageShop.Product.Application.Commands;
using ImageShop.Product.Application.Queries;
using ImageShop.Product.Dtos.CommonDtos;
using ImageShop.Product.Dtos.ReviewDtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImageShop.Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Get list of reviews for a product
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <returns>A list of reviews for a product</returns>
        [ProducesResponseType(typeof(List<ReviewDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string productId)
        {
            var query = new GetReviewsQuery()
            {
               ProductId =productId
            };

            var respose = await _mediator.Send(query);

            if (respose == null || !respose.ProcessedSuccessfully)
                return BadRequest(respose?.ErrorMessage);

            return Ok(respose.Payload);
        }

        /// <summary>
        /// Create a new review for a product
        /// </summary>
        /// <param name="newReviewDto"> Information regarding the new review</param>
        /// <returns>Id of the review</returns>
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateReviewDto newReviewDto)
        {
            var command = new CreateReviewCommand()
            {
                NewReview = newReviewDto,
                Owner = new UserDto()
                {
                    Name = "Authenticated User Name",
                    Email = "test@something.com"
                }
            };

            var response = await _mediator.Send(command);

            if (response == null || !response.ProcessedSuccessfully)
                return BadRequest(response?.ErrorMessage);

            return Ok(response.Payload);
        }
    }
}
