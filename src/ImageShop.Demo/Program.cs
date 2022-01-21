using ImageShop.Product.Domain.ProductAggregate;
using System;
using System.Linq;

namespace ImageShop.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var c in Category.List())
                Console.WriteLine(c);

            foreach (var c in Category.GetAll<Category>())
                Console.WriteLine(c);
        }
    }
}
