using ImageShop.Product.Dtos.CommonDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShop.Product.Dtos.EventDtos
{
    public abstract class BaseCommand
    {
        public UserDto Owner { get; set; }
    }
}
