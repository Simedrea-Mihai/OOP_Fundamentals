using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Traits : BaseEntity
    {
        public override int Id { get; set; }
        public double ExtraOvr { get; set; }
        public string Description { get; set; }

        public Traits(double extraOvr, string description)
        {
            ExtraOvr = extraOvr;
            Description = description;
        }
    }
}
