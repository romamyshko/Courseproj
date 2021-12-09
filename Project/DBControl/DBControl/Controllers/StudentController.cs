﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBControl.Models;

namespace DBControl.Controllers
{
    internal class StudentController
    {
        private readonly UniversityContext _context;

        public StudentController(DbConnectionInfo connection)
        {
            _context = new UniversityContext(connection);
        }

        public int Insert(Student student)
        {
            try
            {
                _context.Add(student);
                return _context.SaveChanges();
            }
            catch
            {
                return -1;
            }
        }

        public int Update(Student updatedStudent)
        {
            try
            {
                _context.Update(updatedStudent);
                return _context.SaveChanges();
            }
            catch
            {
                return -1;
            }
        }

        public Student Get(int id)
        {
            return _context.Students.Find(id);
        }

        public int Delete(int id)
        {
            try
            {
                Student student = _context.Students.Find(id);

                if (DeleteAllStudentsSubjectsByStudentId(student.StudentId) != -1)
                {
                    _context.Students.Remove(student);
                }
                    
                return _context.SaveChanges();
            }
            catch
            {
                return -1;
            }
        }

        private int DeleteAllStudentsSubjectsByStudentId(int studentId)
        {
            try
            {
                IQueryable<StudentSubject> studentSubjectList = _context.StudentsSubjects.Where(id => id.StudentId == studentId); // id.StudentId or id.Student.StudentId ??

                foreach (var studentSubject in studentSubjectList)
                {
                    _context.StudentsSubjects.Remove(studentSubject);
                }

                return _context.SaveChanges();
            }
            catch
            {
                return -1;
            }
        }
    }
}