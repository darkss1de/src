using System;
using System.IO;

class Program
{
    static void Main()
    {
        string[] sentences = new string[3];
        
        // Читаем три предложения из файла "sentences.txt"
        // Предполагается, что файл уже существует и содержит нужные предложения
        try
        {
            using (StreamReader sr = new StreamReader("sentences.txt"))
            {
                for (int i = 0; i < 3; i++)
                {
                    sentences[i] = sr.ReadLine();
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл не найден.");
            return;
        }
        
        // Выводим предложения в обратном порядке
        for (int i = 2; i >= 0; i--)
        {
            Console.WriteLine(sentences[i]);
        }
        
        Console.ReadLine();
    }
}