using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShop.Common.Abstractions
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetIndividualComponents();

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var valueObj = (ValueObject)obj;

            return this.GetIndividualComponents().SequenceEqual(valueObj.GetIndividualComponents());
        }

        public override int GetHashCode()
        {
            return GetIndividualComponents().Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return left?.Equals(right) ?? false;
        }

        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !(left?.Equals(right) ?? false);
        }
    }
}
