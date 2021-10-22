using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballManager.Entities;
using FootballManager.Enums;
using FootballManager.EntitiesLogic;

namespace FootballManager
{
    class Program
    {
        static void Main(string[] args)
        {

            ///*
            //List<int> teams = new List<int>();
            //teams.Add(1);
            //Player player = new Player("Cristiano", "Ronaldo", DateTime.Now, 1, 500, Enums.PlayerPosition.ST, teams);
            //Player player2 = new Player("Cristiano2", "Ronaldo", DateTime.Now, 1, 500, Enums.PlayerPosition.ST, teams);

            //List<Player> players = new List<Player>();
            //players.Add(player);

            //Manager manager = new Manager("Luis", "Enrique", DateTime.Now, teams);

            //Supporter supporter = new Supporter(0);
            //List<Supporter> supporters = new List<Supporter>();
            //supporters.Add(supporter);

            //Team team = new Team(0, manager, players, supporters);

            //Player.Generate(players, 100);
            //players.Show(firstName: true, birthDate : true, playerPosition : true);

            //team.AddPlayers(players);*/

            //List<Player> players = new List<Player>();


            //players.AddPlayer(new Player());
            //players.AddPlayer(new Player("Lionel", "Messi", DateTime.Now, 169, 67, 0, 500, PlayerPosition.RW, new List<int>()));
            //Player.Generate(players, 100);
            //Player.Generate(players, 1000);


            ////players.Show(firstName: true, lastName: true, goals: true, height: true, weight: true, playerPosition: true, status: true);

            //Console.WriteLine("\n\n\n");


            //List<Supporter> supporters = new List<Supporter>();
            //List<int> teams = new List<int>(); // o sa fie ceva dictionary maybe, nu doar un int 
            //Manager manager = new Manager("Luis", "Enrique", DateTime.Now, 180, 70, teams);

            //Team team = new Team(1, "FC Barcelona", manager, supporters);

            //team.Scout(players, new int[] { 50, 3, 10, 10, 15 });
            //Console.Write(team.Players.Count);

            //team.Players.Show(firstName: true, lastName: true, birthDate: true, currentTeamId: true, playerPosition: true, status: true);

            //Console.WriteLine("\n\n\n");
            //players.Show(firstName: true, lastName: true, goals: true, height: true, weight: true, playerPosition: true, status: true);


            List<Player> players = new List<Player>(); // a list of players
            Player.Generate(players, 50); // generate 1000 random players

            Manager manager1 = new Manager("Luis", "Enrique", DateTime.Now, 170, 67, new List<int>());  // create a manager
            List<Supporter> supporters = new List<Supporter>(); // create a list of supporters

            Team team = new Team(1, "FC Barcelona", manager1, supporters); // create the team with index 1

            //team.Scout(players, new int[] { 50, 3, 20, 10, 10 }); // scout new players

            //team.Players.Show(playerPosition: true, ovr: true, age: true);

            Console.WriteLine("\n\n\n");

            team.Scout(players, new int[] { 10, 5, 1, 1, 1 });

            var x = team.Players.Select(player => new { player.FirstName, player.LastName, player.Age, player.Potential, player.OVR, player.Position });
            foreach (var item in x)
            {
                Console.WriteLine(item.FirstName + "      " + item.LastName + "      " + item.Age + "      " + item.OVR + " -> " + item.Potential + "      " + item.Position);
            }


            


        }
    }
}
