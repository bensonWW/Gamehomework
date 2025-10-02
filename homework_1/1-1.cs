using System;
namespace Homework
{
    internal class Q1
    {
        public void Run()
        {
            Console.Write("Enter index(0~4):");
            int input = int.Parse(Console.ReadLine());
            string studentsText = "¦¨°û°] 27 91 21 33 13\r\n¯Î¶®´@ 96 90 40 55 69\r\n°Kºû¯ø 38 85 72 13 34\r\n¶À¤h­õ 81 40 24 93 79\r\n³¢¯\¬À 72 33 32 83 73\r\n³¯»öÚ{ 78 55 22 41 62\r\n§õºÑ«Û 30 48 13 93 70\r\n±ç°·¥É 23 89 10 44 24\r\n³\¶®²Q 90 11 33 27 67\r\n¿½©{·s 29 64 64 90 43";
            string[] Students = studentsText.Split("\r\n");
            for (int i = 0; i < Students.Length; i++)
            {
                for(int j = i;j < Students.Length; j++)
                {
                    if (int.Parse(Students[i].Split(' ')[input + 1]) <= int.Parse(Students[j].Split(' ')[input + 1])) {
                        string tmp = Students[i];
                        Students[i] = Students[j];
                        Students[j] = tmp;
                    }
                }
                Console.WriteLine(Students[i]);
            }
        }
    }

}