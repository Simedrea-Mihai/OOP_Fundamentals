using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.DTOs
{
    public class LeagueGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TeamGetDto> Teams { get; set; }
    }
}
