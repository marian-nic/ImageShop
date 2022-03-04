using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShop.Product.Dtos.EventDtos
{
    public class CommandResponseDto<T>
    {
        public T Payload { get; set; }

        public string ErrorMessage { get; set; }

        public bool ProcessedSuccessfully { get; set; }
    }
}
