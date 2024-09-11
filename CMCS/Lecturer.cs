using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CMCS
{
    internal class Lecturer
    {
        public int LecturerId { get; set; }
        public string Name { get; set; }
        public List<Claim> Claims { get; set; } = new List<Claim>();
    }
}
