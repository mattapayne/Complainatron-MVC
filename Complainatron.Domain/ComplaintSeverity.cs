using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Complainatron.Domain
{
    public class ComplaintSeverity : BaseEntity
    {
        public string Name { get; set; }
        public bool IsDefault { get; set; }

        public ComplaintSeverity()
        {

        }
    }
}
