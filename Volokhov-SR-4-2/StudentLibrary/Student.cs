using System;
using System.Text;
using System.Text.Json;
using System.Linq;
namespace StudentLibrary
{
    // Общий комментарий(хочу +баллы за альтернативку): было бы логичнее создать какой-нибудь StudentsException и
    // FacultyException, а то и в конструкторе, и в перегрузке оператора используется один и тот же ArgumentException.
    // С разными Exception их легче ловить.
   [Serializable]
    public class Student
    {
        private string name;
        /// <summary>
        /// Имя студента.
        /// </summary>
        public string Name { get => name; private set => name = value; }

        private Faculty faculty;
        /// <summary>
        /// Факультет студента.
        /// </summary>
        public Faculty Faculty { get => faculty; set => faculty = value; }

        private double mark;
        /// <summary>
        /// Оценка студента.
        /// </summary>
        public double Mark { get => mark; private set => mark = value; }

        /// <summary>
        /// Конструктор класса Student.
        /// </summary>
        /// <param name="name">Имя студента</param>
        /// <param name="faculty">Факультет студента</param>
        /// <param name="mark">Оценка студента</param>
        public Student(string name, Faculty faculty, double mark)
        {
            // Задание через свойства значений полям, выполняя проверку входных параметров.
            Name = name.Length >= 6 && name.Length <= 10 ? name : throw new ArgumentException("Длина имени должна лежать в диапозоне [6; 10]");
            Faculty = faculty;
            Mark = mark >= 4 && mark < 10 ? mark : throw new ArgumentException("Значение оценки должно лежать в диапозоне [4; 10)");
        }

        /// <summary>
        /// Вывод информации о студенте в формате строки.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Faculty} Student {Name}: Mark = {Mark:f3}";
        }

        public static Student operator +(Student firstStudent, Student secondStudent)
        {
            // Альтернативки ради альтернативок: можно было использовать StringBuilder.
            // И еще среднее значение оценки можно было найти, инийиализировав массив оценок и через Linq найти Average от него.
            // Но я не захотел выделять лишнюю память.

            // Создание нового имени студента. Первая половина самого длинного имени + вторая половина самого короткого имени.
            string newName = firstStudent.Name.Length > secondStudent.Name.Length ?
                                   firstStudent.Name.Substring(0, (int)Math.Ceiling(firstStudent.Name.Length / 2.0)) +
                                                secondStudent.Name.Substring((int)Math.Ceiling(secondStudent.Name.Length / 2.0)) :
                                   secondStudent.Name.Substring(0, (int)Math.Ceiling(secondStudent.Name.Length / 2.0)) +
                                                firstStudent.Name.Substring((int)Math.Ceiling(firstStudent.Name.Length / 2.0));

            // Возвращение нового объекта класса Student.
            return new Student(newName,
                               firstStudent.Faculty == secondStudent.Faculty ? firstStudent.Faculty : throw new ArgumentException("Факультеты студентов не должны различаться"),
                               (firstStudent.Mark + secondStudent.Mark) / 2);
        }
    }
}
