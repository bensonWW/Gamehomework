using System;

namespace Homework;

internal class Q1
{
    internal void initial(List<int> computerBingolist, List<int> playerBingolist, int side)
    {
        for (int i = 1; i <= side * side; i++)
        {
            computerBingolist.Add(i);
            playerBingolist.Add(i);
        }
        for (int i = 0;i < side * side; i++)
        {
            int random = Random.Shared.Next(i, side * side);
            (computerBingolist[i], computerBingolist[random]) = (computerBingolist[random], computerBingolist[i]);
        }
        for (int i = 0; i < side * side; i++)
        {
            int random = Random.Shared.Next(i, side * side);
            (playerBingolist[i], playerBingolist[random]) = (playerBingolist[random], playerBingolist[i]);
        }
    }
    internal int horizontal(List<int> bingoList, int side)
    {
        int line = 0;
        for (int i = 0; i < side; i++)
        {
            for (int j = 0; j < side; j++)
            {
                if (bingoList[i*side + j] != 0)
                {
                    break;
                }   
                else if (j == side - 1)
                {
                    line++;
                }
            }
        }
        return line;
    }
    internal int straight(List<int> bingoList, int side)
    {
        int line = 0;
        for (int i = 0; i < side; i++)
        {
            for (int j = 0; j < side; j++)
            {
                if (bingoList[i + side*j] != 0)
                {
                    break;
                }
                else if (j == side - 1)
                {
                    line++;
                }
            }
        }
        return line;
    }
    internal int oblique(List<int> bingoList, int side)
    {
        int line = 0;

        for (int i = 0; i < side; i++)
        {
            if (bingoList[i * (side + 1)] != 0)
            {
                break;
            }
            else if (i == side - 1)
            {
                line++;
            }
        }
        for (int i = 0; i < side; i++)
        {
            if (bingoList[(i + 1) * (side - 1)] != 0)
            {
                break;
            }
            else if (i == side - 1)
            {
                line++;
            }
        }

        return line;
    }
    internal bool checkBingo(List<int> bingoList,int side)
    {
        return (horizontal(bingoList, side) + straight(bingoList, side) + oblique(bingoList,side)) >= side;
    }
    internal void print(List<int> bingoList,int side)
    {
        for (int i = 0; i < side; i++) 
        {
            for(int j = 0;j < side; j++)
            {
                Console.Write("+----");
            }
            Console.WriteLine("+");
            for (int j = 0; j < side; j++)
            {
                Console.Write($"| {bingoList[i * side + j],-2} ");
            }
            Console.WriteLine("|");
        }
        for (int j = 0; j < side; j++)
        {
            Console.Write("+----");
        }
        Console.WriteLine("+");
    }
    internal void computerMark(List<int> playerBingolist,List<int> computerBingolist)
    {
        List<int>markList = new List<int>();
        for (int i = 0;i < computerBingolist.Count; i++)
        {
            if(computerBingolist[i] != 0)
            {
                markList.Add(i);
            }
        }
        int random = Random.Shared.Next(0,markList.Count);
        Console.WriteLine($"電腦輸入數字:{computerBingolist[markList[random]]}");
        playerBingolist[playerBingolist.FindIndex(x => x == computerBingolist[markList[random]])] = 0;
        computerBingolist[markList[random]] = 0;
        
    }
    internal void playerMark(List<int> playerBingolist, List<int> computerBingolist,int input)
    {
        int pIdx = playerBingolist.FindIndex(x => x == input);
        if (pIdx >= 0) playerBingolist[pIdx] = 0;

        int cIdx = computerBingolist.FindIndex(x => x == input);
        if (cIdx >= 0) computerBingolist[cIdx] = 0;
    }
    internal void Run()
    {
        Console.WriteLine("請輸入side:");
        int side = int.Parse(Console.ReadLine());
        List<int> computerBingolist = new List<int>();
        List<int> playerBingolist = new List<int>();
        initial(computerBingolist, playerBingolist,side);
        print(playerBingolist,side);
        print(computerBingolist, side);
        while ((!checkBingo(computerBingolist, side) && !checkBingo(playerBingolist, side)))
        {
            Console.WriteLine("請輸入數字:");
            int input = int.Parse(Console.ReadLine());
            playerMark(playerBingolist,computerBingolist,input);
            print(playerBingolist, side);
            print(computerBingolist, side);
            computerMark(playerBingolist,computerBingolist);
            print(playerBingolist, side);
            print(computerBingolist, side);
        }
        
    }
}