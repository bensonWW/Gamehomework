namespace Homework;

internal class Q1
{
    public void Run()
    {
        int[][] array = new int[10][];
        int size = 0;
        for(int i = 0;i < 10; i++)
        {
            array[i] = new int[i + 1];
            for (int j = 0; j <= i; j++)
            {  
                array[i][j] = size++;
                Console.Write(array[i][j]);
            }
            Console.WriteLine();
        }
    }
}