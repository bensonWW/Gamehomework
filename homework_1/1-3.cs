namespace Homework
{
    internal class Q3
    {
        public void Run()
        {
            for (int i = 1; i < 10; i++) {
                for (int j = 1; j < 10; j++) { 
                    if(j > 9 - i){
                        Console.Write($"{i}x{j}={i*j,2} ");
                    }
                    else{
                        Console.Write($"       ");
                    }
                }
                Console.WriteLine();
            }
        }
    } 
}