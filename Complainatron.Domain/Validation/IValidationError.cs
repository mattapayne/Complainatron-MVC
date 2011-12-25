using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Complainatron.Domain.Validation
{
    public interface IValidationError
    {
        string Property { get; }
        string ErrorMessage { get; }
    }
}
