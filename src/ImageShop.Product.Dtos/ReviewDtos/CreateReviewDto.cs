using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShop.Product.Dtos.ReviewDtos
{
    public class CreateReviewDto
    {
        public string Description { get; set; }
        public int Score { get; set; }
        public string ProductId { get; set; }
    }
}
