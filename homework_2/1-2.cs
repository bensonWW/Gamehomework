namespace Homework;
internal class Q2
{
    public void Run()
    {
        int[] array = new int[20];
        for(int i = 0;i <= 19; i++)
        {
            array[i] = Random.Shared.Next(0,100);
        }
        Console.WriteLine($"[{string.Join(",", array)}]");
        for (int i = 0;i <=2 ; i++)
        {
            int random = Random.Shared.Next(i,20);
            int temp = array[i];
            array[i] = array[random];
            array[random] = temp;
            Console.WriteLine($"[{string.Join(",", array)}]");
        }

    }
}