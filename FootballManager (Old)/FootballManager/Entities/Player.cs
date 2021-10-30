using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using FootballManager.Entities;
using FootballManager.Enums;
using FootballManager.EntitiesLogic;
using FootballManager.ExternalClasses;

namespace FootballManager.Entities
{
    public class Player : Person
    {
        Random rnd = new Random();

        public Player() : base()
        {
            this.CurrentTeamId = 0;
            this.Goals = 0;
            this.Position = (PlayerPosition)rnd.Next(0, 13);
            this.FormerTeamsIds = new List<int>();
            this.Status = false;
            this.OVR = rnd.Next(55, 65);
            this.Potential = getPotential(this.Age);
        }


        public Player(string firstName, string lastName, DateTime birthDate, int height, int weight,  
                      int currentTeamId,
                      int goals,
                      PlayerPosition position,
                      List<int> formerTeamsIds,
                      int ovr,
                      int potential) : base(firstName, lastName, birthDate, height, weight)
        {

            this.CurrentTeamId = currentTeamId;
            this.Goals = goals;
            this.Position = position;
            this.FormerTeamsIds = formerTeamsIds;
            this.Status = true;
            this.OVR = ovr;
            this.Potential = potential;

        }

        public int CurrentTeamId { get; set; }
        public int Goals { get; set; }
        public PlayerPosition Position { get; set; }
        public List<int> FormerTeamsIds { get; set; }
        public bool Status { get; set; }

        public int OVR { get; set; }
        public int Potential { get; set; }


        // -----------------------------------------------------------------------------------------------

        // --- STATIC METHODS ---

        // Generate players and add them to the list
        public static void Generate(List<Player> players, int number)
        {
            for (int i = 0; i < number; i++)
            {
                players.AddPlayer(new Player());
                players[i].FirstName = PersonData.GetRandomFirstName().Result;
                players[i].LastName = PersonData.GetRandomLastName().Result;
            }
        }

        // -----------------------------------------------------------------------------------------------

        bool between(int age, int x, int y)
        {
            if (x <= age && age <= y)
                return true;
            else
                return false;
        }

        int potentialGenerator(int[] chances)
        {
            int[] prob = new int[11];
            int k = 0, number = 1;
            int sum = 0;

            Exception e = new Exception("The size of the array does not correspond to the given format {int[4]}");

            if (chances.Length != 4)
                throw e;

            for (int i = 0; i < chances.Length; i++)
                sum += chances[i];

            Exception e2 = new Exception($"10 != {sum}. The sum of the values from the given array must be equl 10");

            if (sum != 10)
                throw e2; 


            for (int i = 0; i < chances.Length;i++)
            {
                while(chances[i] > 0)
                {
                    prob[k] = number;

                    k += 1;
                    chances[i]--;
                }
                number += 1;
            }

            int choice = rnd.Next(0, 10);

            if (prob[choice] == 1)
                return rnd.Next(90, 96);
            else if (prob[choice] == 2)
                return rnd.Next(85, 90);
            else if (prob[choice] == 3)
                return rnd.Next(80, 85);
            else if (prob[choice] == 4)
                return rnd.Next(70, 80);
            else
                return 0;
        }

        public int getPotential(int age)
        {
            if (between(age, 15, 20))
                return potentialGenerator(new int[] { 7, 2, 1, 0 });

            else if (between(age, 21, 25))
                return potentialGenerator(new int[] { 5, 3, 2, 0 });

            else if (between(age, 26, 30))
                return potentialGenerator(new int[] { 1, 6, 3, 0 });

            else if (between(age, 31, 100))
                return potentialGenerator(new int[] { 0, 0, 5, 5 });

            return 0;
        }


    }
}
