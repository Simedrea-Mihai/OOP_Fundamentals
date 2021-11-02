﻿using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Player : BaseEntity
    {
        public override int Id { get; set; }
        public Profile Profile { get; set; }

        public PlayerAttribute PlayerAttribute { get; set; }

        public bool Free_Agent { get; set; }

        private Player() { }

        public Player(Profile profile)
        {
            Profile = profile;
        }

    }
}
