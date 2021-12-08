using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Npgsql;

namespace DataGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConnectionInfo connection = GetConnectionFromJson("../../../../../../data/application.json");
            Run(connection);
        }

        static ConnectionInfo GetConnectionFromJson(string filePathToJson)
        {
            return JsonSerializer.Deserialize<ConnectionInfo>(new StreamReader(filePathToJson).ReadToEnd());
        }

        static void Run(ConnectionInfo connectionInfo)
        {
            List<Faculty> faculties = JsonSerializer.Deserialize<List<Faculty>>(new StreamReader("../../../../../../data/faculties.json").ReadToEnd());

            List<string> courses = new List<string>();
            List<string> subjects = new List<string>();

            GetSeperateCsvData("../../../../../../data/courses_subjects.csv", ref courses, ref subjects);

            List<string> names = new List<string>();
            List<string> surnames = new List<string>();

            GetSeperateCsvData("../../../../../../data/names_surnames.csv", ref names, ref surnames);



            NpgsqlConnection connection = new NpgsqlConnection(connectionInfo.Connection);

            connection.Open();

            RunSQLCommands(faculties, courses, subjects, names, surnames, connection);

            connection.Close();
        }

        static void RunSQLCommands(List<Faculty> faculties, List<string> courses,
            List<string> subjects, List<string> names, List<string> surnames, NpgsqlConnection connection)
        {
            Random r = new Random();

            Faculty[] facultiesArr = faculties.ToArray();
            string[] coursesArr = courses.ToArray();
            string[] subjectsArr = subjects.ToArray();
            string[] namesArr = names.ToArray();
            string[] surnamesArr = surnames.ToArray();

            for (int i = 0; i < surnamesArr.Length; i++)
            {
                var insertFaculty = new NpgsqlCommand($"INSERT INTO faculties (name) VALUES('{facultiesArr[r.Next(facultiesArr.Length - 1)].Name}') RETURNING faculty_id", connection);
                int facultyId = (int)insertFaculty.ExecuteScalar()!;

                var insertSubject = new NpgsqlCommand($"INSERT INTO subjects (name) VALUES('{subjectsArr[r.Next(subjectsArr.Length - 1)]}') RETURNING subject_id", connection);
                int subjectId = (int)insertSubject.ExecuteScalar()!;

                var insertCourse = new NpgsqlCommand($"INSERT INTO courses (faculty_id, name) VALUES({facultyId}, '{coursesArr[r.Next(coursesArr.Length - 1)]}') RETURNING course_id", connection);
                int courseId = (int)insertCourse.ExecuteScalar()!;

                var insertGroup = new NpgsqlCommand($"INSERT INTO groups (course_id, name) VALUES({courseId}, '{(char)r.Next(66, 90)}{(char)r.Next(65, 90)}-{r.Next(11, 90)}{r.Next(11, 90)}') RETURNING group_id", connection);
                int groupId = (int)insertGroup.ExecuteScalar()!;

                var insertStudent = new NpgsqlCommand($"INSERT INTO students (group_id,fullname) VALUES({groupId}, '{namesArr[r.Next(namesArr.Length - 1)] + " " + surnamesArr[r.Next(surnamesArr.Length - 1)]}') RETURNING student_id", connection);
                int studentId = (int)insertStudent.ExecuteScalar()!;

                var insertStudentSubject = new NpgsqlCommand($"INSERT INTO students_subjects (student_id, subject_id, mark) VALUES({studentId}, {subjectId}, {r.Next(60, 100)})", connection);
                insertStudentSubject.ExecuteScalar();

            }
        }

        static void GetSeperateCsvData(string filepath, ref List<string> list1, ref List<string> list2)
        {
            string[] csv = File.ReadAllLines(filepath);

            foreach (var s in csv)
            {
                string[] splitted = s.Split(',', 2);

                if (splitted[0] != "")
                {
                    list1.Add(splitted[0]);
                }

                if (splitted[1] != "")
                {
                    list2.Add(splitted[1]);
                }
            }
        }
    }
}