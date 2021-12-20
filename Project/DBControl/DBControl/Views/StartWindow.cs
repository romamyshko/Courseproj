using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBControl.Models;

namespace DBControl.Views
{
    internal class StartWindow
    {
        private readonly UniversityContext _context;

        public StartWindow(UniversityContext context)
        {
            _context = context;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("_______University_DataBase_Control_Program_______\r\n");
                Console.WriteLine("Choose working option:\r\n\t[1] Add a new student\r\n\t[2] Edit the existing student\r\n\t[3] Find the student\r\n\t[4] Get all students\r\n\r\nWrite 'exit' to end program.");
                
                if (ChooseWorkingOption(Console.ReadLine()) == 0)
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
                        var studentWindow = new ShowStudentWindow(_context);
                        studentWindow.Run();
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
