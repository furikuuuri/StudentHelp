using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sheldy.Models;
using Sheldy.Helpers;

namespace Sheldy.Repositories
{
    public class UserRepository:IUserRepository<User>
    {
        private SheldyContext db;

        public UserRepository()
        {
            this.db = new SheldyContext();
        }

        public IEnumerable<User> GetUserList()
        {
            return db.Users;
        }

        public User GetUser(User user)
        {
            return db.Users.Include(prop => prop.Role).SingleOrDefault(u => u.Username == user.Username && HashString.GetHashString(user.Password) == u.Password);
  
        }

        public User Create(User user)
        {
            user.Password = HashString.GetHashString(user.Password);
            user.Role = db.Roles.First(p => p.RoleName == "user");
            db.Users.Add(user);
            Save();
            return user;
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
