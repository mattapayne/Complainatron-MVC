using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Complainatron.Domain.Validation;

namespace Complainatron.Domain
{
    public class Complaint : BaseEntity
    {
        public string ComplaintText { get; set; }
        public long FacebookUserId { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public string FacebookUserName { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ComplaintSeverity Severity { get; set; }
        public Guid ComplaintSeverityId { get; set; }

        public Complaint()
        {
            Tags = new List<Tag>();
        }

        public override IEnumerable<IValidationError> ValidateForCreate()
        {
            var errors = base.ValidateForCreate().ToList();

            if (String.IsNullOrEmpty(ComplaintText))
            {
                errors.Add(new ValidationError("ComplaintText", "Complaint text is required."));
            }

            if (FacebookUserId <= 0)
            {
                errors.Add(new ValidationError("FacebookUserId", "The Facebook user id is required."));
            }

            if(String.IsNullOrEmpty(FacebookUserName))
            {
                errors.Add(new ValidationError("FacebookUserName", "The Facebook user name is required."));
            }

            if (Severity == null)
            {
                errors.Add(new ValidationError("Severity", "Complaint severity is required."));
            }

            return errors;
        }

        public override IEnumerable<IValidationError> ValidateForUpdate()
        {
            return ValidateForCreate();
        }
    }
}
