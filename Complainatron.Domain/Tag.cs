using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Complainatron.Domain
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Complaint> Complaints { get; set; }

        public Tag()
        {
            Complaints = new List<Complaint>();
        }
    }
}
