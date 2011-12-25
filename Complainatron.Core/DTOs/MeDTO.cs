using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Complainatron.Core.DTOs
{
    public class MeDTO
    {
        public long FacebookUserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PicureUrl { get; set; }
    }
}
