using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Profile : BaseEntity
    {
        public override int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }

        public Player Player { get; set; }
        public int? PlayerId { get; set; }

        public Manager Manager { get; set; }
        public int? ManagerId { get; set; }


        public Profile() { }

        public Profile(string firstName, string lastName, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }

    }
}
