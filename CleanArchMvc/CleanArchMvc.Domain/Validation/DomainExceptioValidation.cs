using System;

namespace CleanArchMvc.Domain.Validation
{
    public class DomainExceptioValidation : Exception
    {
        public DomainExceptioValidation(string error):base(error)
        {}

        public static void When(bool hasError, string error)
        {
            if (hasError)
                throw new DomainExceptioValidation(error);
        }

    }
}
