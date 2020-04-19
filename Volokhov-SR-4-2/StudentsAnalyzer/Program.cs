using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using StudentLibrary;
using Utils;

namespace StudentsAnalyzer
{
    class Program
    {
        // Константы.
        const string JSON_FILE_DIRECTORY = "../../../../students.json";

        static void Main(string[] args)
        {
            // Инициализация списка студентов.
            List<Student> studentsList = new List<Student>();

            // Чтение данных о студентах из файла JSON_FILE_DIRECTORY и сохранение в прочитанных студентов в studentsList.
            using (StreamReader file = File.OpenText(JSON_FILE_DIRECTORY))
            {
                try { 
                    Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                    studentsList = (List<Student>)serializer.Deserialize(file, typeof(List<Student>));
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
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"ArgumentException: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: непредвиденная ошибка");
                }

            }

            // Вывод в консоль информации о количестве студентов МИЭМ.
            Console.WriteLine("Number of MIEM students: {0}", studentsList.Count((Student student) => student.Faculty == Faculty.MIEM));

            // Сортировка студентов по оценке по убыванию и вывод их в консоль.
            studentsList.OrderByDescending(student => student.Mark).Take(10).ToList().
                ForEach(student => Console.WriteLine(student));


            // Группировка студентов по факультетам и их сложение.
            List<Student> studentsGroupped = studentsList.GroupBy((Student student) => student.Faculty).
                                                Select(student => student.Aggregate((firstStudent, secondStudent) => firstStudent + secondStudent)).ToList();

            // Вывод в консоль студентов, сгруппированных и сложенных по факультетам.
            Console.WriteLine(Environment.NewLine + "Groupped Students:");
            studentsGroupped.ForEach(student => Console.WriteLine(student));

            // Сортировка studentsGroupped по оценке по убыванию и вывод их в консоль.

            // Из задания непонятно, по Name сортировать по возрастанию или по убыванию, но по правилам русского языка
            // должна иметься в виду сортировка по Name по убыванию. Также не сказано про необходимость вывода в консоль
            // получившегося списка, так что я не стал этого делать.
            List<Student> studentsGrouppedAndSorted = studentsGroupped.OrderByDescending(student => student.Mark).
                                                                       ThenByDescending(student => student.Name).ToList();
        }
    }
}
