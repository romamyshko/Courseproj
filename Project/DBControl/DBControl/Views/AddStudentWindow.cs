using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBControl.Models;

namespace DBControl.Views
{
    internal class AddStudentWindow
    {
        private readonly UniversityContext _context;

        public AddStudentWindow(UniversityContext context)
        {
            _context = context;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("_______University_DataBase_Control_Program_______\r\n");
                Console.WriteLine("You can write 'back' to return to the main window.");
                Console.WriteLine("Write some information about student below:");
                Console.Write("Full name: ");
                string fullname = Console.ReadLine();

                string faculty = "";
                EnterFaculty(faculty);

                string course = "";
                EnterCourse(course, faculty);

                string group = "";
                EnterGroup(group, course);
                
                if (ChooseWorkingOption(Console.ReadLine()) == 0)
                {
                    return;
                }
            }
        }

        private void EnterFaculty(string faculty)
        {
            while (true)
            {
                Console.Write("Faculty: ");
                faculty = Console.ReadLine();
                if (_context.Faculties.Count(elem => elem.Name.Equals(faculty)) != 0)
                {
                    return;
                }
            }
        }

        private void EnterCourse(string course, string faculty)
        {
            while (true)
            {
                Console.Write("Studying course: ");
                course = Console.ReadLine();
                if (_context.Courses.Count(elem => elem.Name.Equals(course) && elem.Faculty.Name.Equals(faculty)) != 0)
                {
                    return;
                }
            }
        }

        private void EnterGroup(string group, string course)
        {
            while (true)
            {
                Console.Write("Group: ");
                group = Console.ReadLine();
                if (_context.Groups.Count(elem => elem.Name.Equals(group) && elem.Course.Name.Equals(course)) != 0)
                {
                    return;
                }
            }
        }

        private int ChooseWorkingOption(string input)
        {
            if (int.TryParse(input, out var number) && number is >= 1 and <= 4)
            {
                switch (number)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                }

                return 1;
            }

            return input is "exit" ? 0 : -1;
        }
    }
}
