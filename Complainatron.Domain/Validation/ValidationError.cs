using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Complainatron.Domain.Validation
{
    public class ValidationError : IValidationError
    {
        private readonly string _property;
        private readonly string _errorMessage;

        public ValidationError(string property, string errorMessage)
        {
            _property = property;
            _errorMessage = errorMessage;
        }

        public string Property
        {
            get { return _property; }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
        }
    }
}
