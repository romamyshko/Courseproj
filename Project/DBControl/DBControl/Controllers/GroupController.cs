using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBControl.Models;

namespace DBControl.Controllers
{
    internal class GroupController
    {
        private readonly DbConnectionInfo _connection;
        private readonly UniversityContext _context;

        public GroupController(DbConnectionInfo connection)
        {
            _connection = connection;
            _context = new UniversityContext(_connection);
        }

        public int Insert(Group group)
        {
            try
            {
                _context.Add(group);
                return _context.SaveChanges();
            }
            catch (Exception)
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
                _context.Groups.Remove(group);
                return _context.SaveChanges();
            }
            catch
            {
                return -1;
            }
        }
    }
}
