using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Complainatron.Domain
{
    public class User : BaseEntity
    {
        public long FacebookId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
