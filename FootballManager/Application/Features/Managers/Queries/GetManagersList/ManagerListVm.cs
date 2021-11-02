using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain;

namespace Application.Features.Managers.Queries.GetManagersList
{
    public class ManagerListVm
    {
        public int Id { get; set; }
        public Profile Profile { get; set; }
    }
}
