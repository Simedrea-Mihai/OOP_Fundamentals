using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.DTOs
{
    public class TeamGetDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HeaderDescription { get; set; }
        public double Budget { get; set; }
        public List<PlayerGetDto> Players { get; set; }
        public ManagerGetDto Manager { get; set; }
    }
}
