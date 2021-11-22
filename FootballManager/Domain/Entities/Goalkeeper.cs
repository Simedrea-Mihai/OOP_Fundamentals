using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Goalkeeper : GoalkeeperProfile
    {
        public int OVR { get; set; }
        public int CleanSheet { get; set; }
        public int DIV { get; set; }
        public int REF { get; set; }
        public int HAN { get; set; }
        public int POS { get; set; }
    }
}
