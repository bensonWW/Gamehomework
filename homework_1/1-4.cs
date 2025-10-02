using System;
using System.Collections.Generic;
using System.Linq;

namespace Homework
{
    internal class Q4
    {
        public void Run()
        {
            int[][] marks = new int[][] {
                new int[]{1,2,3,4,5,6,7,8,9},
                new int[]{1,2,9},
                new int[]{1,3,8,9},
                new int[]{1,4,7,8,9},
                new int[]{1,2,3,4,5,6,7,8,9},
                new int[]{1,4,7,8,9},
                new int[]{1,3,8,9},
                new int[]{1,2,9},
                new int[]{1,2,3,4,5,6,7,8,9}
            };
            for (int i = 1; i <= 9; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    if (Array.IndexOf(marks[i - 1], j) >= 0)
                    {
                        if (j == 1)
                            Console.Write($"{i}x{j}={i * j} ");
                        else
                            Console.Write($"{i}x{j}={i * j,2} ");
                    }
                    else
                    {
                        if (j == 1)
                            Console.Write($"      ");
                        else
                            Console.Write($"       ");
                    }
                }
                Console.WriteLine();
            }

        }
    }
}
