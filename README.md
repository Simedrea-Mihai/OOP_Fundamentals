# Football Manager
## Main Program

<details>
<summary>Program.cs</summary>
<br>
  
```csharp
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

```
</details>
  
## Classes

### Entities
  
<details>
<summary>Manager.cs</summary>
<br>
  
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Entities
{
    public class Manager : Person
    {
        public Manager(string firstName, string lastName, DateTime birthDate, int height, int weight,
                       List<int> trainedTeamsIds) : base(firstName, lastName, birthDate, height, weight)
        {
            this.TrainedTeamsIds = trainedTeamsIds;
        }

        public List<int> TrainedTeamsIds;
    }
}

```
</details>

<details>
<summary>Person.cs</summary>
<br>
  
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FootballManager.ExternalClasses;

namespace FootballManager.Entities
{
    public abstract class Person
    {

        private Random rnd = new Random();

        // Person constructor with random params
        public Person()
        {
            this.FirstName = PersonData.GetRandomFirstName();
            this.LastName = PersonData.GetRandomLastName();
            this.BirthDate = GenDateTime.DRandom();
            this.Height = rnd.Next(150, 210);
            this.Weight = rnd.Next(50, 250);
        }

        // Person constructor with params
        public Person(string firstName, string lastName, DateTime birthDate, int height, int weight)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            this.Height = height;
            this.Weight = weight;
        }


        // Properties
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public short Age { get; }
        public int Salary { get; set; }
    }

    public static class PersonData
    {
        private static string[] firstNamesData = System.IO.File.ReadAllLines(@"firstNamesDB.txt");
        private static string[] lastNamesData = System.IO.File.ReadAllLines(@"lastNamesDB.txt");
        private static Random rnd = new();

        public static string GetRandomFirstName()
        {
            return firstNamesData[rnd.Next(0, firstNamesData.Length)];
        }

        public static string GetRandomLastName()
        {
            return lastNamesData[rnd.Next(0, lastNamesData.Length)];
        }
    }
}

```
</details>

<details>
<summary>Player.cs</summary>
<br>
  
```csharp
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
        }


        public Player(string firstName, string lastName, DateTime birthDate, int height, int weight,  
                      int currentTeamId,
                      int goals,
                      PlayerPosition position,
                      List<int> formerTeamsIds) : base(firstName, lastName, birthDate, height, weight)
        {

            this.CurrentTeamId = currentTeamId;
            this.Goals = goals;
            this.Position = position;
            this.FormerTeamsIds = formerTeamsIds;
            this.Status = true;

        }

        public int CurrentTeamId { get; set; }
        public int Goals { get; set; }
        public PlayerPosition Position { get; set; }
        public List<int> FormerTeamsIds { get; set; }
        public bool Status { get; set; }


        // -----------------------------------------------------------------------------------------------

        // --- STATIC METHODS ---

        // Generate players and add them to the list
        public static void Generate(List<Player> players, int number)
        {
            for (int i = 1; i <= number; i++)
                players.AddPlayer(new Player());
        }

        
    }
}

```
</details>

<details>
<summary>Supporter.cs</summary>
<br>
  
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Entities
{
    public class Supporter
    {
        public Supporter(int favoriteTeamId)
        {
            this.FavoriteTeamId = favoriteTeamId;
        }

        public int FavoriteTeamId;

       
    }
}
```
</details>

<details>
<summary>Team.cs</summary>
<br>
  
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FootballManager.Entities;
using FootballManager.EntitiesLogic;
using FootballManager.Enums;
using FootballManager.ExternalClasses;

namespace FootballManager.Entities
{
    public class Team
    {
        public Team(int teamId, string name, Manager manager, List<Supporter> supporters)
        {
            this.TeamId = teamId;
            this.Name = name;
            this.Manager_Team = manager;
            this.Supporters = supporters;
            this.Players = new List<Player>();
        }

        public int TeamId { get; }
        public string Name { get; set; }
        public Manager Manager_Team { get; set; }
        public List<Player> Players { get; set; }
        public List<Supporter> Supporters { get; set; }



        // ----------------------------------------------------------------------------------------------

        // Add team methods
        public void Scout(List<Player> players, int[] playersDistribution) 
        {
            int totalNumber = playersDistribution[0];
            int gk_count = playersDistribution[1];
            int defender_count = playersDistribution[2];
            int midfielder_count = playersDistribution[3];
            int striker_count = playersDistribution[4];

            foreach(Player player in players.ToList())
            {
                if(gk_count > 0 && player.Position == PlayerPosition.GK)
                {
                    player.Status = true;
                    player.CurrentTeamId = TeamId;
                    Players.AddPlayer(player);
                    gk_count--;
                    totalNumber--;
                }

                else if(defender_count > 0 && (player.Position == PlayerPosition.CB || player.Position == PlayerPosition.RB
                                            || player.Position == PlayerPosition.LB || player.Position == PlayerPosition.LWB)
                                            || player.Position == PlayerPosition.RWB)
                {
                    player.Status = true;
                    player.CurrentTeamId = TeamId;
                    Players.AddPlayer(player);
                    defender_count--;
                    totalNumber--;
                }

                else if(midfielder_count > 0 && (player.Position == PlayerPosition.LM || player.Position == PlayerPosition.RM
                                              || player.Position == PlayerPosition.CM || player.Position == PlayerPosition.CAM))
                {
                    player.Status = true;
                    player.CurrentTeamId = TeamId;
                    Players.AddPlayer(player);
                    midfielder_count--;
                    totalNumber--;
                }

                else if(striker_count > 0 && (player.Position == PlayerPosition.LW || player.Position == PlayerPosition.RW
                                           || player.Position == PlayerPosition.CF || player.Position == PlayerPosition.ST))
                {
                    player.Status = true;
                    player.CurrentTeamId = TeamId;
                    Players.AddPlayer(player);
                    striker_count--;
                    totalNumber--;
                }
                else
                {
                    if(totalNumber > 0)
                    {
                        player.Status = true;
                        player.CurrentTeamId = TeamId;
                        players.AddPlayer(player);
                        totalNumber--;
                    }
                }
            }

        }
    }
}

```
</details>

### EntitiesLogic

<details>
<summary>PlayerLogic.cs</summary>
<br>
  
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FootballManager.Entities;
using FootballManager.Enums;

namespace FootballManager.EntitiesLogic
{

    // List Extensions for "Player" class
    public static class ListExtensions
    {
        public static void AddPlayer(this List<Player> players, Player player)
        {
            // there will be some requirements 
            players.Add(player);
        }

        public static void Show(this List<Player> players, bool firstName = false, bool lastName = false, bool birthDate = false,
                 bool height = false, bool weight = false, bool currentTeamId = false, bool goals = false, bool playerPosition = false, bool formerTeamsIds = false, bool status = false)
        {
            bool[] values = new bool[] { firstName, lastName, birthDate, height, weight, currentTeamId, goals, playerPosition, formerTeamsIds, status };

            for(int i = 0; i < players.Count(); i++)
            {
                if (values[0])
                    Console.Write(" | " + players[i].FirstName + " | ");

                if (values[1])
                    Console.Write(" | " + players[i].LastName + " | ");

                if (values[2])
                    Console.Write(" | " + players[i].BirthDate + " | ");

                if (values[3])
                    Console.Write(" | " + players[i].Height + " | ");

                if (values[4])
                    Console.Write(" | " + players[i].Weight + " | ");

                if (values[5])
                    Console.Write(" | " + players[i].CurrentTeamId + " | ");

                if (values[6])
                    Console.Write(" | " + players[i].Goals + " | ");

                if (values[7])
                    Console.Write(" | " + players[i].Position + " | ");

                if (values[8])
                    Console.Write(" | " + players[i].FormerTeamsIds + " | ");

                if (values[9])
                    Console.Write(" | " + players[i].Status + " | ");


                Console.WriteLine();




            }

        }

        private static readonly List<Player> _players = new();
        public static IReadOnlyList<Player> Players => _players.AsReadOnly();

    }

    public class PlayerLogic
    {
      

    }
}

```
</details>
  
<details>
<summary>SupporterLogic.cs</summary>
<br>
  
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballManager.Entities;

namespace FootballManager.EntitiesLogic
{
    public interface ISupporterLogic
    {
        public void Add_Supporters(Supporter supporter)
        {

        }
    }

    public class SupporterLogic : ISupporterLogic
    {
        public SupporterLogic()
        {

        }
    }
}

```
</details>

### Enums

<details>
<summary>PlayerPosition.cs</summary>
<br>
  
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Enums
{
    public enum PlayerPosition
    {
         GK, CB, LB, RB, LWB, RWB, CM, CAM, LM, RM, CF, ST, LW, RW
    }
}

```
</details>
  
### ExternalClasses

<details>
<summary>GenDateTime.cs</summary>
<br>
  
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.ExternalClasses
{
    public static class GenDateTime
    {
        public static DateTime DRandom()
        {
            Random rnd = new Random();
            DateTime dateTime = new DateTime(rnd.Next(1975, DateTime.Now.Year - 15), rnd.Next(1, 12), rnd.Next(1, 28)); // aici e ceva problema
            return dateTime;
        }
    }
}

```
</details>


  
## UML Diagram
  
  ![Alt text here](UML.drawio.png)
