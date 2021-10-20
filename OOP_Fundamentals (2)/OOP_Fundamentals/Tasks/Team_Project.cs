using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Fundamentals.Tasks
{


    public class Team_Project
    {
        // Properties

        private readonly Manager _manager;
        private bool isprosperous = false;

        public bool IsProsperous 
        {
            get { return isprosperous; }
            set 
            {
                isprosperous = value;

                if (value)
                    _manager.Wage += 1;
                else
                    _manager.Wage -= 1;
            } 
        }
       

        // Constructor of class "Team_Project" without params
        public Team_Project() {}

        // Constructor of class "Team_Project" with 1 param
        public Team_Project(Manager manager)
        {
            _manager = manager;
        }

    }
}
