namespace Homework
{
    internal class Q5
    {
        public void Run()
        {
            int randomnum = Random.Shared.Next(1, 100);
            int max = 100, min = 0;

            Console.Write($"�п�J���� ({min}, {max}) �������Ʀr: ");
            int input = int.Parse(Console.ReadLine());

            while (input != randomnum)
            {
                if (input < randomnum) min = input;
                else if (input > randomnum) max = input;

                Console.Write($"�п�J���� ({min}, {max}) �������Ʀr: ");
                input = int.Parse(Console.ReadLine());
            }

            Console.WriteLine($"�A�o��F���סG{randomnum}");

        }
    }
}