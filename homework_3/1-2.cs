namespace Homework;
internal class Q2
{
    internal bool iscorrect(int num)
    {
        string i_s = num.ToString("D4");
        for (int i = 0; i <= 3; i++)
        {
            for (int j = 0; j <= 3; j++)
            {
                if (i_s[i] == i_s[j] && i != j)
                {
                    return false;
                }
            }
        }
        return true;
    }
    internal void initial(List<string> legalstringList)
    {
        for (int i = 0; i <= 10000; i++)
        {
            if (iscorrect(i))
            {
                legalstringList.Add(i.ToString("D4"));
            }
        }
    }
    internal void check(string input, string targetNum)
    {
        int A = 0, B = 0;
        for (int i = 0; i <= 3; i++)
        {
            if (input[i] == targetNum[i])
            {
                A += 1;
            }
            else if (targetNum.Contains(input[i]))
            {
                B += 1;
            }
        }
        Console.WriteLine($"A:{A}B:{B}");
    }

    internal void Run()
    {
        List<string> legalstringList = new List<string>();
        initial(legalstringList);
        int random = Random.Shared.Next(0, legalstringList.Count);
        string targetNum = legalstringList[random];
        Console.WriteLine($"(����)���׬�:{targetNum}");
        Console.WriteLine("�п�J4��Ʀr:");
        string input = Console.ReadLine();
        while (input != targetNum)
        {
            check(input, targetNum);
            Console.WriteLine("�п�J4��Ʀr:");
            input = Console.ReadLine();

        }
        Console.WriteLine($"�A����F���׬O{targetNum}");
        Console.WriteLine($"�ŦX���Ʀr��{legalstringList.Count}��");
    }
}