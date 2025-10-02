using System.Security.Principal;
using Homework;
internal class Q4
{
    internal void Run()
    {
        Console.Write("請輸入side:");
        Bingo bingo = new Bingo(int.Parse(Console.ReadLine()));
        bingo.play();
    }
    
}
internal class Bingo
{
    internal Bingo(int side)
    {
        this.series = new int[side * side];
        this.marks = new bool[side * side];
        this.nmubers = new Stack<int>();
        this.side = side;
        for (int i = 0;i < side *side; i++)
        {
            this.series[i] = i+1;
            this.marks[i] = false;
        }
        this.shuffle();
    }
    internal void shuffle()
    {
        for(int i = 0; i < this.side * this.side;i++)
        {
            int temp = this.series[i];
            int random = Random.Shared.Next(i,this.side*this.side);
            this.series[i] = this.series[random];
            this.series[random] = temp;
        }
    }
    internal void play() 
    {
        int input = -1;
        this.shuffle();
        while(input != 0 && !go()){
            this.show();
            Console.Write("請輸入數字:");
            input = int.Parse(Console.ReadLine());
            for (int i = 0;i < this.side * this.side; i++)
            {
                if (this.series[i] == input) {
                    if (marks[i])
                    {
                        Console.WriteLine("已mark過這個數字請再輸入一次");
                    }
                    else{
                        this.markCell(i);
                        this.nmubers.Push(input);
                    }
                    break;
                }

            }
        }
    }
    internal bool go()
    {
        int count = 0;
        bool straight = true,Horizontal = true, oblique = true;
        //判斷直的
        for (int i = 0;i < this.side;i++)
        {
            for(int j = 0; j < this.side; j++)
            {
                straight = straight && marks[i + j * side];
            }
            if (straight)
            {
                count++;
            }
            straight = true;
        }
        //判斷橫的
        for (int i = 0; i < this.side; i++)
        {
            for (int j = 0; j < this.side; j++)
            {
                Horizontal = Horizontal && marks[side * i + j];
            }
            if (Horizontal)
            {
                count++;
            }
            Horizontal = true;
        }
        //判斷斜的
        for (int i = 0; i < this.side; i++)
        {
            oblique = oblique && marks[side * i + i];
        }
        if (oblique)
        {
            count++;
        }
        oblique = true;
        for (int i = side;i >= 1; i--)
        {
            oblique = oblique && marks[side * i - i];
        }
        if (oblique)
        {
            count++;
        }
        if (count >= 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    internal void markCell(int k) 
    {
        this.marks[k] = true;
    }
    internal void show() 
    {
        for(int i = 0; i < this.side; i++)
        {
            for (int j = 0; j < side; j++) { Console.Write("+----"); }
            Console.WriteLine("+");
            for (int j = 0;j < this.side; j++)
            {
                if (this.marks[this.side * i + j])
                {
                    Console.Write("| @  ");
                }
                else
                {
                    Console.Write($"| {this.series[this.side * i + j],-2} ");
                }
            }
            Console.WriteLine("|");
        }
        for (int j = 0; j < side; j++) { Console.Write("+----"); }
        Console.WriteLine("+");
    }
    internal void reset() 
    {
        this.series = new int[0];
        this.marks = new bool[0];
        this.nmubers = new Stack<int>();
    }
    private int side;
    private int[] series;
    private Stack<int> nmubers;
    private bool[] marks;
}