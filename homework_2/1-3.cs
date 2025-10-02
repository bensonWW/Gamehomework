namespace Homework;

internal class Q3
{
    internal void Run()
    {
        int[] Bingolist = new int[81];
        for(int i = 0;i<= 80; i++)
        {
            Bingolist[i] = i;
        }
        for (int i = 0;i <= 80; i++)
        {
            int random = Random.Shared.Next(i,81);
            int temp = Bingolist[i];
            Bingolist[i] = Bingolist[random];
            Bingolist[random] = temp;
        }
        for(int i = 0;i <= 8; i++)
        {
            Console.WriteLine("+----+----+----+----+----+----+----+----+----+");
            for (int j = 0;j <= 8; j++)
            {
                Console.Write($"| {Bingolist[9*i + j],-2} ");
            }
            Console.WriteLine("|");
        }
        Console.WriteLine("+----+----+----+----+----+----+----+----+----+");
    }
}