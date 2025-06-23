using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Быки и коровы! 🐂🐄");
        Console.WriteLine("Правила:\nЭто логическая игра, в которой игрок загадывает число из разных цифр (обычно 4), а компьютер пытается его угадать.\nПосле каждой попытки игрок получает подсказку: количество быков 🐂 (верная цифра и позиция) и коров 🐄 (верная цифра, но не позиция). Цель — угадать число, получив 4 быка.\n");
        string num;
        while (true)
        {
            num = Console.ReadLine();
            if (num.Length == 4 && num.All(char.IsDigit))
        }


    }
}
