﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBControl.Models;

namespace DBControl.Controllers
{
    internal class GroupController
    {
        private readonly UniversityContext _context;

        public GroupController(DbConnectionInfo connection)
        {
            _context = new UniversityContext(connection);
        }

        public int Insert(Group group)
        {
            try
            {
                _context.Add(group);
                return _context.SaveChanges();
            }
            catch
            {
                return -1;
            }
        }

        public int Update(Group updatedGroup)
        {
            try
            {
                _context.Update(updatedGroup);
                return _context.SaveChanges();
            }
            catch
            {
                return -1;
            }
        }

        public Group Get(int id)
        {
            return _context.Groups.Find(id);
        }

        public int Delete(int id)
        {
            try
            {
                Group group = _context.Groups.Find(id);

                if (DeleteAllStudentsByGroupId(group.GroupId) != -1)
                {
                    _context.Groups.Remove(group);
                }
                
                return _context.SaveChanges();
            }
            catch
            {
                return -1;
            }
        }

        private int DeleteAllStudentsByGroupId(int groupId)
        {
            try
            {
                IQueryable<Student> studentList = _context.Students.Where(id => id.GroupId == groupId); // id.GroupId or id.Group.GroupId ??

                foreach (var student in studentList)
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
    }
}
