using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

using StudentLibrary;
using Utils;

namespace StudentGenerator
{
    class Program
    {
        // Константы.
        const int MIN_NAME_LENGTH = 6;
        const int MAX_NAME_LENGTH = 10;
        const int MIN_MARK = 4;
        const int MAX_MARK = 10;
        const int STUDENTS_COUNT = 30;
        // Так как я пишу на .NET Core, то файл надо поднимать на 4 уровня вверх, в отличии от .NET Framework.
        const string JSON_FILE_DIRECTORY = "../../../../students.json";

        /// <summary>
        /// Генерирует случайное имя.
        /// </summary>
        /// <param name="nameLength">Количество символов в имени</param>
        /// <returns></returns>
        static string GenerateName(int nameLength)
        {
            // Инициализация переменной генерируемого имени.
            StringBuilder nameBuilder = new StringBuilder();

            // Добавление заглавной буквы имени.
            nameBuilder.Append((char)utils.random.Next('A', 'Z' + 1));

            // Добавление остальных букв имени.
            for (int i = 0; i < nameLength - 1; i++)
            {
                nameBuilder.Append((char)utils.random.Next('a', 'z' + 1));
            }

            return nameBuilder.ToString();
        }

        static void Main(string[] args)
        {
            // Инициализация списка студентов.
            List<Student> studentsList = new List<Student>();

            // Добавление 30 случайных студентов в studentsList.
            for (int i = 0; i < STUDENTS_COUNT; i++)
            {
                // Генерация случайного студента и добавление его в studentsList.
                studentsList.Add(new Student(GenerateName(utils.random.Next(MIN_NAME_LENGTH, MAX_NAME_LENGTH + 1)),
                                             (Faculty)utils.random.Next(0, 3),
                                             utils.random.Next(MIN_MARK, MAX_MARK) + utils.random.NextDouble()));
            }

            // Сохранение данных о студентах в формате json в файл JSON_FILE_DIRECTORY.
            using (Newtonsoft.Json.JsonTextWriter fs = new Newtonsoft.Json.JsonTextWriter(new StreamWriter(JSON_FILE_DIRECTORY)))
            {
                try
                {
                    Newtonsoft.Json.JsonSerializer jsonSerializer = new Newtonsoft.Json.JsonSerializer();
                    jsonSerializer.Serialize(fs, studentsList);
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine("FileNotFoundException: файл не найден");
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine("UnauthorizedAccessException: ошибка доступа");
                }
                catch (IOException ex)
                {
                    Console.WriteLine("IOException: ошибка ввода/вывода");
                }
                catch (System.Security.SecurityException ex)
                {
                    Console.WriteLine("SecurityException: ошибка безопасности");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: непредвиденная ошибка");
                }
            }
        }
    }
}
