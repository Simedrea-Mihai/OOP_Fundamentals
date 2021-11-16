﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class League : BaseEntity
    {
        public override int Id { get; set; }
        public IList<Team> Teams { get; set; }
        public string Name { get; set; }

        public League(string name)
        {
            Name = name;
        }

    }
}
