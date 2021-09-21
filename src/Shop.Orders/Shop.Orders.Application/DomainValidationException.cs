using System;

namespace Shop.Orders.Api
{
    public class DomainValidationException : Exception
    {
        public DomainValidationException(string error)
            : base(error)
        {
        }
    }

}
