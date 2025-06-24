using System;
using System.Text;

class Program
{
    static void Main(string[] args)
    {


        Console.WriteLine("Быки и коровы! 🐂🐄");
        Console.WriteLine("Правила:\nЭто логическая игра, в которой компьютер загадывает число из разных цифр, а игрок пытается его угадать.\nПосле каждой попытки игрок получает подсказку: количество быков 🐂 (верная цифра и позиция) и коров 🐄 (верная цифра, но не позиция). Цель — угадать число, получив 4 быка.\n");
        Console.WriteLine("Нажмите любую клавишу для продолжения");
        Console.ReadKey();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("🐂 Выбор режима:\n");
            Console.WriteLine("1. Легчайший режим (бесконечные попытки)");
            Console.WriteLine("2. Классика (10 попыток)");
            Console.WriteLine("3. Пользовательский (задайте кол-во попыток)");
            Console.WriteLine("4. Инверсия (вы загадываете число, компьютер угадывает)");
            Console.Write("\nВведите номер режима (1–4): ");

            string input = Console.ReadLine()?.Trim();

            switch (input)
            {
                case "1":
                    AllModes(1);
                    break;
                case "2":
                    AllModes(2);
                    break;
                case "3":
                    AllModes(3);
                    break;
                case "4":
                    InverseMode();
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Нажмите любую клавишу и попробуйте снова.");
                    Console.ReadKey();
                    continue;
            }

            Console.Write("\nХотите сыграть снова? (yes): ");
            var answer = Console.ReadLine()?.Trim().ToLower();
            if (answer != "yes")
                break;
        }



    }

    static void Recur(List<int> current, bool[] used, List<string> result)
    {
        if (current.Count == 4)
        {
            result.Add(string.Join("", current));
            return;
        }

        for (int i = 0; i < 10; i++)
        {
            if (!used[i])
            {
                used[i] = true;
                current.Add(i);

                Recur(current, used, result);

                used[i] = false;
                current.RemoveAt(current.Count - 1);
            }
        }
    }

    static void AllModes(int mode)
    {
        int maxatt = mode == 2 || mode == 1 ? 10 : GetMaxAttem();
        Console.Clear();
        Console.WriteLine($"Запущен {(mode == 1 ? "легкий" : (mode == 2 ? "классический" : "пользовательский"))} режим");
        string num = GenerateNumber();
        int attempt = 0;
        //Console.WriteLine(num);//ТЕСТ!!!!!!!!!!!!!!

        while (true)
        {
            string guess = GetUserGuess();
            attempt++;

            int bulls = CountBulls(num, guess);
            int cows = CountCows(num, guess);
            if (bulls == 4)
            {
                Console.WriteLine("Победа! 🐂🐄");
                Console.WriteLine("Нажмите любую клавишу для продолжения");
                Console.ReadKey();
                break;
            }
            else if (attempt == maxatt && mode != 1)
            {
                Console.WriteLine("Поражение( 🐂🐄");
                Console.WriteLine($"Загаданное число: {num}");
                Console.WriteLine("Нажмите любую клавишу для продолжения");
                Console.ReadKey();
                break;
            }
            else
            {
                string bullsIcons = string.Concat(Enumerable.Repeat("🐂", bulls));
                string cowsIcons = string.Concat(Enumerable.Repeat("🐄", cows));

                Console.WriteLine($"Попытка {attempt}: {guess} -{((bulls == 0 && cows == 0) ? "" : "")}{bullsIcons}{cowsIcons} ({bulls} бык{CorrectLangRu(bulls, cows).Item1},{cows} коров{CorrectLangRu(bulls, cows).Item2})");
            }
        }
    }
    static void InverseMode()
    {
        List<string> result = new List<string>();
        List<int> current = new List<int>();
        bool[] used = new bool[10];
        Recur(current, used, result);

        Console.Clear();
        Console.WriteLine("Запущен режим инверсия");
        Console.WriteLine("Загадайте число\n");
        int attempt = 1;

        Console.WriteLine($"Попытка {attempt}: 0123");
        string guess = "0123";
        (int bulls, int cows) = GetBullsAndCows();
        if (bulls == 4)
        {
            Console.WriteLine("Число угадано!");
            return;
        }
        result = result.Where(num =>
            CountBulls(num, guess) == bulls &&
            CountCows(num, guess) == cows).ToList();
        while (true)
        {
            attempt++;
            if (result.Count == 0)
            {
                Console.WriteLine("Ошибка");
                break;
            }
            guess = result[0];

            Console.WriteLine($"Попытка {attempt}: {guess}");
            (bulls, cows) = GetBullsAndCows();
            if (bulls == 4)
            {
                Console.WriteLine("Число угадано!");
                break;
            }
            result = result.Where(num =>
                CountBulls(num, guess) == bulls &&
                CountCows(num, guess) == cows).ToList();
        }


    }

    static (int bulls, int cows) GetBullsAndCows()
    {
        while (true)
        {
            int bulls;
            int cows;
            Console.Write("🐂 Введите количество быков: ");
            if (!int.TryParse(Console.ReadLine()?.Trim(), out bulls) && bulls >= 0 && bulls <= 4)
            {
                Console.WriteLine("Неверный ввод: введите целые числа от 0 до 4\n");
                continue;
            }
            Console.Write("🐄 Введите количество коров: ");
            if (!int.TryParse(Console.ReadLine()?.Trim(), out cows) && cows >= 0 && cows <= 4)
            {
                Console.WriteLine("Неверный ввод: введите целые числа от 0 до 4\n");
                continue;
            }
            if (bulls + cows > 4)
            {
                Console.WriteLine("Сумма быков и коров не может превышать 4\n");
                continue;
            }
            return (bulls, cows);
        }
    }


    static string GenerateNumber()
    {
        int[] data = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
        Random.Shared.Shuffle(data);
        return string.Concat(data.Take(4));
    }
    static int GetMaxAttem()
    {
        while (true)
        {
            Console.Write("Введите количество попыток: ");
            string? input = Console.ReadLine()?.Trim();

            if (!int.TryParse(input, out int attempts))
            {
                Console.Clear();
                Console.WriteLine("Введите целое число.\n");
                continue;
            }
            if (attempts <= 0)
            {
                Console.Clear();
                Console.WriteLine("Количество попыток должно быть больше нуля.\n");
                continue;
            }

            return attempts;

        }
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
            if (guess.Contains(num[i])&&num[i] != guess[i]) cows++;
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
