﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain;

namespace Application.Contracts.Persistence
{
    public interface IManagerRepository
    {
        Manager Create(Manager manager);
        IList<Manager> ListAll();
        IList<Manager> ListFreeManagers();
        IList<Manager> ListTakenManagers();
    }
}
