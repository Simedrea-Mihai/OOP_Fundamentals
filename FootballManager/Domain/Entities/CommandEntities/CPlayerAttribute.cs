using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CommandEntities
{
    public class CPlayerAttribute
    {

        public int OVR { get; set; }
        public int Potential { get; set; }


        public CPlayerAttribute(int ovr, int potential)
        {
            OVR = ovr;
            Potential = potential;
        }
    }
}
