namespace Homework;
internal class Q3
{
    // 這裡用小寫 main 照你的介面；若要當程式進入點請改成 public static void Main()
    internal void Run()
    {
        Console.Write("輸入數字:");
        int N = int.Parse(Console.ReadLine());
        var game = new Game1A2B(N);
        string answer = game.GenNumber(N);
        Console.WriteLine($"答案為:{answer}");
        while (true)
        {
            Console.Write("\nYour guess (or 0 to exit): ");
            string guess = Console.ReadLine()!.Trim();

            if (guess == "0") break;

            if (!game.IsAvailable(guess))
            {
                Console.WriteLine("✗ 不合法：需為不重複的數字，長度正確且首位不可為 0。");
                continue;
            }

            var ab = game.GetAB(guess, answer);
            Console.WriteLine($"{guess} => {ab[0]}A{ab[1]}B");

            if (ab[0] == N)
            {
                Console.WriteLine("You've got the answer!");
                break;
            }
        }
    }
}

class Game1A2B
{
    private string[] nos = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
    private int numberLen;
    private string answer = "";

    // 建構函式，決定遊戲數字的位數(N):
    public Game1A2B(int N) { numberLen = N; }

    // 回傳指定長度且符合規則的數字:
    public string GenNumber(int n)
    {
        if (n < 1 || n > 10) throw new ArgumentOutOfRangeException(nameof(n));

        // 先洗牌
        Shuffle(nos);

        // 保證第一位不是 0：如果 0 在第 0 位，和非 0 的第一個位置互換
        if (nos[0] == "0")
        {
            int k = Array.FindIndex(nos, 1, s => s != "0");
            (nos[0], nos[k]) = (nos[k], nos[0]);
        }

        answer = string.Concat(nos.Take(n));
        return answer;
    }

    // 檢視給定的 number 字串，是不是符合規則的數字字串:
    public bool IsAvailable(string number)
    {
        if (string.IsNullOrEmpty(number)) return false;
        if (number.Length != numberLen) return false;
        if (number[0] == '0') return false;                 // 首位不能為 0
        if (!number.All(char.IsDigit)) return false;        // 都要是數字

        // 不能重複
        return number.Distinct().Count() == number.Length;
    }

    // 回傳比較 guess 與 answer 是 幾A幾B 的結果:
    public int[] GetAB(string guess, string answer)
    {
        if (guess.Length != answer.Length)
            throw new ArgumentException("guess 與 answer 長度需相同");

        int A = 0, B = 0;
        // 逐位比：位置相同且數字相同 => A
        for (int i = 0; i < guess.Length; i++)
            if (guess[i] == answer[i]) A++;

        // 任意位置數字重疊總數（由於規則是「不重複數字」，可用雙迴圈）
        for (int i = 0; i < guess.Length; i++)
            for (int j = 0; j < answer.Length; j++)
                if (guess[i] == answer[j] && i != j) B++;

        return new[] { A, B };
    }

    // 洗牌（Fisher–Yates），協助生成符合規則的數字:
    public void Shuffle(string[] number)
    {
        // 從尾到頭，每次把 i 與 [0..i] 的隨機位置交換
        for (int i = number.Length - 1; i > 0; i--)
        {
            int j = Random.Shared.Next(i + 1); // 0..i
            (number[i], number[j]) = (number[j], number[i]);
        }
    }
}