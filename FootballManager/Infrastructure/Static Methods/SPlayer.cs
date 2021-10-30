using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain;

namespace Infrastructure.Static_Methods
{
    public static class SPlayer
    {
        static bool between(int age, int x, int y)
        {
            if (x <= age && age <= y)
                return true;
            else
                return false;
        }

        static int potentialGenerator(int[] chances)
        {
            Random rnd = new Random();
            int[] prob = new int[11];
            int k = 0, number = 1;
            int sum = 0;


            if (chances.Length != 4)
                throw new Exception("The size of the array does not correspond to the given format {int[4]}");

            for (int i = 0; i < chances.Length; i++)
                sum += chances[i];


            if (sum != 10)
                throw new Exception($"10 != {sum}. The sum of the values from the given array must be equl 10"); 


            for (int i = 0; i < chances.Length; i++)
            {
                while (chances[i] > 0)
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



        public static int SetPotential(Player player)
        {

            if (between(player.Age, 15, 20))
                return potentialGenerator(new int[] { 7, 2, 1, 0 });

            else if (between(player.Age, 21, 25))
                return potentialGenerator(new int[] { 5, 3, 2, 0 });

            else if (between(player.Age, 26, 30))
                return potentialGenerator(new int[] { 1, 6, 3, 0 });

            else if (between(player.Age, 31, 100))
                return potentialGenerator(new int[] { 0, 0, 5, 5 });

            return 0;

        }
    }
}
