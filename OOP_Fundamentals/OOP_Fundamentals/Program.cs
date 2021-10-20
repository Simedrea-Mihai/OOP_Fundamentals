using System;
using OOP_Fundamentals.Tasks;
using System.Collections.Generic;
using System.Text;

namespace OOP_Fundamentals
{
    class Program
    {
        static void Main(string[] args)
        {

            // Initialization
            List<string> teams = new List<string>();
            teams.Add("Real Madrid"); teams.Add("Atletico Madrid"); teams.Add("FC Barcelona");

            List<Player> players = new List<Player>();


            players.Add(new Player("Real Madrid", 20, teams));
            players.Add(new Player("PSG", 20, teams));
            players.Add(new Player("Man UTD", 20, teams));

            // Set players names
            players[0].Name = "Eden Hazard";
            players[1].Name = "Lionel Messi";
            players[2].Name = "Cristiano Ronaldo";

            Console.WriteLine("Former team for a player\n-------------------------- ");
            Console.WriteLine(players[0].FormerTeams[1]);
            Console.WriteLine("\n\n\n");

            // <------ ASSOCIATION ------ > 

            // Association (player - accessories)
            Accessories accessories = new Accessories();

            players[1].playerBL.Equip(accessories);
            accessories.Link_With(players[1]);


            // Association (player - supporter)
            Supporter supporter = new Supporter();

            Console.WriteLine("Status between player and supporter\n------------------------------------");
            Console.WriteLine(supporter.GetStatus(players[1]));
            Console.WriteLine(players[1].playerBL.GetStatus(supporter));
            Console.WriteLine("\n\n\n");


            // <------ AGGREGATION ------ > 

            // Aggregation (manager - player)
            Manager manager = new Manager();
            foreach (Player player in players)
                manager.players.Add(player);

            Console.WriteLine("Show all players linked to this manager\n---------------------------------------");
            manager.ShowPlayersNames(); // show all players names linked with this manager


            // <------ COMPOSITION ------ > 

            // Composition (manager - team_project)
            
            
            Manager manager1 = new Manager();
            manager1.Name = "Luis Enrique";
            manager1.BirthDate = new DateTime(2002, 12, 2);
            manager1.Wage = 500;



            Console.WriteLine("\n\n\n");

            Console.WriteLine("If the team project is unsuccessful, then the manager wage will decrease\n---------------------------------------------------------------------------");
            manager1.ManagerStatus(false);
            manager1.ManagerStatus(false);

            Console.WriteLine(manager1.Wage);



        }

        

    }
}
