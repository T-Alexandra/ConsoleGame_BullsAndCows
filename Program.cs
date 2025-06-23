using System;
using System.Text;

class Program
{
    static void Main(string[] args)
    {


        Console.WriteLine("Быки и коровы! 🐂🐄");
        Console.WriteLine("Правила:\nЭто логическая игра, в которой компьютер загадывает число из разных цифр, а игрок пытается его угадать.\nПосле каждой попытки игрок получает подсказку: количество быков 🐂 (верная цифра и позиция) и коров 🐄 (верная цифра, но не позиция). Цель — угадать число, получив 4 быка.\n");

        string num = GenerateNumber();
        int attempt = 0;

        Console.WriteLine(num);//ТЕСТ!!!!!!!!!!!!!!

        while (true)
        {
            string guess = GetUserGuess();
            attempt++;

            int bulls = CountBulls(num, guess);
            int cows = CountCows(num, guess) - bulls;

            if (bulls == 4)
            {
                Console.WriteLine("Победа! 🐂🐄");
                break;
            }
            else
            {
                string bullsIcons = string.Concat(Enumerable.Repeat("🐂", bulls));
                string cowsIcons = string.Concat(Enumerable.Repeat("🐄", cows));

                Console.WriteLine($"Попытка {attempt}: {guess} -{((bulls == 0 && cows == 0) ? "":"")}{bullsIcons}{cowsIcons} ({bulls} бык{CorrectLangRu(bulls, cows).Item1},{cows} коров{CorrectLangRu(bulls, cows).Item2})");
            }
        }

    }
    static string GenerateNumber()
    {
        int[] data = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
        Random.Shared.Shuffle(data);
        return string.Concat(data.Take(4));
    }
    static string GetUserGuess()
    {
        while (true)
        {
            Console.Write("Введите предположение: ");
            string guess = string.Concat(Console.ReadLine().Trim().Replace(" ", "").Distinct());
            if (guess.Length == 4 && guess.All(char.IsDigit))
                return guess;
            else Console.WriteLine("Неверное число, повторите ввод");
        }
    }
    static int CountBulls(string num, string guess)
    {
        int bulls = 0;
        for (int i = 0; i < 4; i++)
        {
            if (num[i] == guess[i])
                bulls++;
        }
        return bulls;
    }

    static int CountCows(string num, string guess)
    {
        int cows = 0;
        for (int i = 0; i < 4; i++)
        {
            if (guess.Contains(num[i])) cows++;
        }
        return cows;
    }
    static (string, string) CorrectLangRu(int bulls, int cows)
    {
        string bull = (bulls == 1) ? "" : (bulls == 0 ? "ов" : "а");
        string cow = (cows == 1) ? "а" : (cows == 0 ? "" : "ы"); ;

        return (bull, cow);

    }


}
