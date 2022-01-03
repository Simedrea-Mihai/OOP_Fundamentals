using Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.DTOs
{
    public class PlayerGetDto
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public int Age { get; set; }
        public int Value { get; set; }
        public PlayerPosition Position { get; set; }
        public int OVR { get; set; }
        public int Potential { get; set; }
    }
}
