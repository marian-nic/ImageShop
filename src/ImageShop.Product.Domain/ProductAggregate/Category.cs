using ImageShop.Common.Abstractions;

namespace ImageShop.Product.Domain.ProductAggregate
{
    public class Category: Enumeration
    {
        #region Enum values

        public static readonly Category NotSpecified = new(1, nameof(NotSpecified));
        public static readonly Category Wallpaper = new(2, nameof(Wallpaper));
        public static readonly Category Icon = new(3, nameof(Icon));
        public static readonly Category Text = new(4, nameof(Text));

        #endregion

    }
}