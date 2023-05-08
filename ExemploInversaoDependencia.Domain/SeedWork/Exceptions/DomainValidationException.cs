using System;

namespace ExemploInversaoDependencia.Domain.SeedWork.Exceptions
{
    public class DomainValidationException : Exception
    {
        public DomainValidationException(string msg)
            : base(msg)
        {
        }

        public DomainValidationException()
            : base()
        {
        }
    }
}
