using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DBControl.Models;

namespace DBControl.Views
{
    internal class ShowStudentWindow
    {
        private readonly UniversityContext _context;

        public ShowStudentWindow(UniversityContext context)
        {
            _context = context;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("_______University_DataBase_Control_Program_______\r\n");
                Console.WriteLine("Enter name and surname in such format: Steve Jobs");

                string[] fullname = GetFullname(Console.ReadLine());

                if (fullname.Length != 2)
                {
                    continue;
                }

                Console.Clear();
                Console.WriteLine("_______University_DataBase_Control_Program_______\r\n");
                Console.WriteLine($"Name: '{fullname[0]}' surname: '{fullname[1]}'");
                while (true)
                {
                    
                }
            }
        }

        private string[] GetFullname(string input)
        {
            return input.Split(' ');
        }
    }
}
