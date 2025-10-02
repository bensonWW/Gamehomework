namespace Homework
{
    internal class Q5
    {
        public void Run()
        {
            int randomnum = Random.Shared.Next(1, 100);
            int max = 100, min = 0;

            Console.Write($"請輸入介於 ({min}, {max}) 之間的數字: ");
            int input = int.Parse(Console.ReadLine());

            while (input != randomnum)
            {
                if (input < randomnum) min = input;
                else if (input > randomnum) max = input;

                Console.Write($"請輸入介於 ({min}, {max}) 之間的數字: ");
                input = int.Parse(Console.ReadLine());
            }

            Console.WriteLine($"你得到了答案：{randomnum}");

        }
    }
}