using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Fundamentals.Tasks
{


    public abstract class Person
    {


        // Properties
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int Wage { get; set; }

        // Constructor of class "Person" without params
        public Person() { }

        // Constructor of class "Person" with 3 params
        public Person(string name, DateTime birthDate, int wage)
        {
            this.Name = name;
            this.BirthDate = birthDate;
            this.Wage = wage;
        }

    }

    // Player interface 
    interface IPlayer
    {
        void Equip(Accessories accessories);
        bool GetStatus(Supporter supporter);
    }

    // Player class (The Player IS A type of Person)
    public class Player : Person
    {
        // Properties
        public string CurrentTeam { get; set; }
        public int Goals { get; set; }
        public List<string> FormerTeams { get; set; }

        public PlayerBL playerBL;

        // Constructor of class "Player" with 1 param
        public Player() 
        {
            this.playerBL = new PlayerBL();
        }

        // Constructor of class "Player" with BL params
        public Player(PlayerBL iplayer)
        {
            this.playerBL = iplayer;
        }
        
        // Constructor of class "Player" with 3 params
        public Player(string currentTeam, int goals, List<string> formerTeams)
        {
            this.playerBL = new PlayerBL();
            this.CurrentTeam = currentTeam;
            this.Goals = goals;
            this.FormerTeams = formerTeams;
        }

    }

    // PlayerBL class (BL for Player)
    public class PlayerBL : IPlayer
    {
        public PlayerBL() { }

        // ASSOCIATION
        public void Equip(Accessories accessories)
        {
            // some code
        }

        // Relationship with supporter
        public bool GetStatus(Supporter supporter)
        {
            return true;
        }


    }

    // Manager class (The Manager IS A type of Person)

    public class Manager : Person
    {
        // Properties
        public List<string> TrainedTeams { get; set; }
        private readonly Team_Project _project;

        // Constructor of class "Manager" without params
        public Manager() 
        {
            this._project = new Team_Project(this);
        }


        // Constructor of class "Manager" with 1 param

        public Manager(List<string> trainedTeams)
        {
            this.TrainedTeams = trainedTeams;
        }

        // AGGREGATION (manager - player)
        public List<Player> players = new List<Player>();

        public void ShowPlayersNames()
        {
            foreach(Player player in players)
                Console.WriteLine(player.Name);
        }

        // COMPOSITION (manager status)

        public void ManagerStatus(bool state)
        {
            if (state == true)
                this._project.IsProsperous = true;
            else
                this._project.IsProsperous = false;
        }
        

    }

    // Supporter class (The Supporter IS A type of Person)
    public class Supporter : Person
    {
        // Properties
        public string FavoriteTeam { get; set; }

        // Constructor of class "Supporter" without params
        public Supporter() { }

        // Constructor of class "Supporter" with 1 param
        public Supporter(string favoriteTeam)
        {
            this.FavoriteTeam = favoriteTeam;
        }

        // Relationship with player
        public bool GetStatus(Player player)
        {
            return true;
        }

    }
}
