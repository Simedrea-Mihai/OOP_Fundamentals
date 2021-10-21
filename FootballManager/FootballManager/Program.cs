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

            /*
            List<int> teams = new List<int>();
            teams.Add(1);
            Player player = new Player("Cristiano", "Ronaldo", DateTime.Now, 1, 500, Enums.PlayerPosition.ST, teams);
            Player player2 = new Player("Cristiano2", "Ronaldo", DateTime.Now, 1, 500, Enums.PlayerPosition.ST, teams);

            List<Player> players = new List<Player>();
            players.Add(player);

            Manager manager = new Manager("Luis", "Enrique", DateTime.Now, teams);

            Supporter supporter = new Supporter(0);
            List<Supporter> supporters = new List<Supporter>();
            supporters.Add(supporter);
            
            Team team = new Team(0, manager, players, supporters);

            Player.Generate(players, 100);
            players.Show(firstName: true, birthDate : true, playerPosition : true);

            team.AddPlayers(players);*/

            List<Player> players = new List<Player>();


            players.AddPlayer(new Player());
            players.AddPlayer(new Player("Lionel", "Messi", DateTime.Now, 169, 67, 0, 500, PlayerPosition.RW, new List<int>()));
            Player.Generate(players, 100);
            Player.Generate(players, 1000);


            players.Show(firstName: true, lastName: true, goals: true, height: true, weight: true, playerPosition: true, status: true);

            Console.WriteLine("\n\n\n");


            List<Supporter> supporters = new List<Supporter>();
            List<int> teams = new List<int>(); // o sa fie ceva dictionary maybe, nu doar un int 
            Manager manager = new Manager("Luis", "Enrique", DateTime.Now, 180, 70, teams);

            Team team = new Team(1, "FC Barcelona", manager, supporters);

            team.Scout(players, new int[] { 50, 3, 10, 10, 15 });

            team.Players.Show(firstName: true, lastName: true, birthDate: true, currentTeamId: true, playerPosition: true, status: true);

            Console.WriteLine("\n\n\n");
            players.Show(firstName: true, lastName: true, goals: true, height: true, weight: true, playerPosition: true, status: true);



        }
    }
}
