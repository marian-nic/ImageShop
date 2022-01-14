using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShop.Common.Abstractions
{
    public abstract class Entity
    {
        public string Id { get; protected set; }

        public DateTimeOffset CreatedAt { get; protected set; }
        public DateTimeOffset ModifiedAt { get; protected set; }


        public override bool Equals(object obj)
        {
            if (obj is not Entity)
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            return Id == ((Entity)obj).Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode() ^ 31;
        }
    }
}
