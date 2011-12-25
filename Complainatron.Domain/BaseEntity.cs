using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Complainatron.Domain.Validation;

namespace Complainatron.Domain
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            DateCreated = DateTime.UtcNow;
        }

        public virtual IEnumerable<IValidationError> ValidateForCreate()
        {
            return Enumerable.Empty<IValidationError>();
        }

        public virtual IEnumerable<IValidationError> ValidateForUpdate()
        {
            return Enumerable.Empty<IValidationError>();
        }

        public virtual IEnumerable<IValidationError> ValidateForDelete()
        {
            return Enumerable.Empty<IValidationError>();
        }
    }
}
