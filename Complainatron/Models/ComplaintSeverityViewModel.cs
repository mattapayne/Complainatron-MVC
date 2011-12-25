using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Complainatron.Models
{
    public class ComplaintSeverityViewModel
    {
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public Guid Id { get; set; }
    }
}