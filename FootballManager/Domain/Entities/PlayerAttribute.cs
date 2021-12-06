﻿using Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    public class PlayerAttribute : BaseEntity
    {
        public override int Id { get; set; }

        public int PlayerId { get; set; }

        public int OVR { get; set; }
        public int Potential { get; set; }
        public PlayerPosition Position { get; set; }

        public Traits Traits { get; set; }
        public PlayerAttribute() { }

        public PlayerAttribute(int ovr, int potential, Traits traits, PlayerPosition position)
        {
            OVR = ovr;
            Potential = potential;
            Traits = traits;
            Position = position;
        }

    }
}
