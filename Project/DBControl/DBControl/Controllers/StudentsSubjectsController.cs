﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBControl.Models;

namespace DBControl.Controllers
{
    internal class StudentsSubjectsController
    {
        private readonly DbConnectionInfo _connection;
        private readonly UniversityContext _context;

        public StudentsSubjectsController(DbConnectionInfo connection)
        {
            _connection = connection;
            _context = new UniversityContext(_connection);
        }

        public int Insert(StudentSubject studentSubject)
        {
            try
            {
                _context.Add(studentSubject);
                return _context.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int Update(StudentSubject updatedStudentSubject)
        {
            try
            {
                _context.Update(updatedStudentSubject);
                return _context.SaveChanges();
            }
            catch
            {
                return -1;
            }
        }

        public StudentSubject Get(int id)
        {
            return _context.StudentsSubjects.Find(id);
        }

        public int Delete(int id)
        {
            try
            {
                StudentSubject studentSubject = _context.StudentsSubjects.Find(id);
                _context.StudentsSubjects.Remove(studentSubject);
                return _context.SaveChanges();
            }
            catch
            {
                return -1;
            }
        }
    }
}
