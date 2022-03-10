using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShop.Product.Application.Exceptions
{
    [Serializable()]
    public  class CustomValidationException : Exception
    {
        public CustomValidationException() : base()
        {
        }

        public CustomValidationException(string message): base(message)
        {
        }

        public CustomValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomValidationException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext): base(serializationInfo, streamingContext)
        {
        }
    }
}
