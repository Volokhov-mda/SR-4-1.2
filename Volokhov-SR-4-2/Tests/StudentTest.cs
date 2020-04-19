using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

using StudentLibrary;

namespace Tests
{
    [TestClass]
    public class StudentTest
    {
        const string JSON_TEST_FILE = "../../../testStudent.json";

        [TestMethod]
        public void TestToString()
        {
            // Arrange.
            Student student = new Student("Vsevolod", Faculty.CS, 4);
            string expected = "CS Student Vsevolod: Mark = 4.000";

            // Act.
            string actual = student.ToString();

            // Assert.
            // Вообще, можно было бы переопределить метод Equals() у класса Student, но тогда было бы
            // сложнее отследить ошибку, так как было бы непонятно, какое конкретно свойство не совпадает с ожидаемым.
            Assert.AreEqual(expected, actual, "Student info is incorrect");
        }

        [TestMethod]
        public void TestPlusOperator()
        {
            // Arrange.
            Student firstStudent = new Student("Vsevolod", Faculty.CS, 4);
            Student secondStudent = new Student("Nikitos", Faculty.CS, 8);
            Student expected = new Student("Vsevitos", Faculty.CS, 6);

            // Act.
            Student actual = firstStudent + secondStudent;

            // Assert.
            // Вообще, можно было бы переопределить метод Equals() у класса Student, но тогда было бы
            // сложнее отследить ошибку, так как было бы непонятно, какое конкретно свойство не совпадает с ожидаемым.
            Assert.AreEqual(expected.Name, actual.Name, "Actual name is incorrect");
            Assert.AreEqual(expected.Faculty, actual.Faculty, "Faculty is incorrect");
            Assert.AreEqual(expected.Mark, actual.Mark, "Mark is incorrect");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPlusOperatorException()
        {
            // Arrange.
            Student firstStudent = new Student("Vsevolod", Faculty.CS, 4);
            Student secondStudent = new Student("Nikitos", Faculty.Design, 8);

            // Act.
            Student mergedStudent = firstStudent + secondStudent;
        }

        [TestMethod]
        public void TestSerialization()
        {
            // Arrange.
            Student expected = new Student("Vsevolod", Faculty.CS, 4);

            // Act.
            using (Newtonsoft.Json.JsonTextWriter fs = new Newtonsoft.Json.JsonTextWriter(new StreamWriter(JSON_TEST_FILE)))
            {
                Newtonsoft.Json.JsonSerializer jsonSerializer = new Newtonsoft.Json.JsonSerializer();
                jsonSerializer.Serialize(fs, expected);
            }

            // Assert.
            Student actual;
            using (StreamReader file = File.OpenText(JSON_TEST_FILE))
            {
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                actual = (Student)serializer.Deserialize(file, typeof(Student));
            }

            Assert.AreEqual(expected.Name, actual.Name, "Actual name is incorrect");
            Assert.AreEqual(expected.Faculty, actual.Faculty, "Faculty is incorrect");
            Assert.AreEqual(expected.Mark, actual.Mark, "Mark is incorrect");
        }
    }
}
