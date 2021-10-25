﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Enums
{
    public static class Formations
    {
        public static int[,] formations = new int[,] { { 2, 3, 5, 0, 0 },
                                                       { 3, 3, 4, 0, 0 },
                                                       { 4, 2, 4, 0, 0 },
                                                       { 4, 4, 2, 0, 0 },
                                                       { 4, 3, 3, 0, 0 },
                                                       { 4, 4, 1, 1, 0 },
                                                       { 4, 3, 1, 2, 0 },
                                                       { 4, 1, 2, 3, 0 },
                                                       { 4, 1, 2, 1, 2 },
                                                       { 4, 1, 3, 2, 0 },
                                                       { 4, 3, 2, 1, 0 },
                                                       { 5, 3, 2, 0, 0 },
                                                       { 1, 4, 3, 2, 0 },
                                                       { 3, 4, 3, 0, 0 },
                                                       { 3, 5, 2, 0, 0 },
                                                       { 3, 4, 1, 2, 0 },
                                                       { 3, 6, 1, 0, 0 },
                                                       { 4, 5, 1, 0, 0 },
                                                       { 4, 2, 3, 1, 0 },
                                                       { 5, 4, 1, 0, 0 },
                                                       { 1, 6, 3, 0, 0 },
                                                       { 4, 2, 2, 2, 0 },
                                                       { 3, 3, 1, 3, 0 },
                                                       { 3, 3, 3, 1, 0 },
                                                       { 4, 2, 1, 3, 0 } };

        static Random rnd = new();

        public static int[] GetFormation()
        { 
            int[] array = new int[5];
            int rndNr = rnd.Next(0, 10);
            for (int i = 0; i < 5; i++)
                array[i] = formations[rndNr, i];
            return array;
        }
    }
}
