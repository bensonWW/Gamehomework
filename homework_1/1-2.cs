namespace Homework
{
    internal class Q2
    {
        public void Run()
        {
            for (int i = 1; i < 10; i++) {
                for (int j = 1; j < 10; j++) {
                    Console.Write($"{i}x{j} = {i * j,2} ");
                }
                Console.WriteLine();
            }
        }
    }
}