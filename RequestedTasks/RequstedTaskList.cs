using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sheldy.Models;

namespace Sheldy.RequestedTasks
{
    public class RequstedTaskList
    {
        SheldyContext db;
        public RequstedTaskList()
        {
            db = new SheldyContext();
        }
        public void AddRequstedTask(RequestedTask requestedTask)
        {
            db.RequestedTasks.Add(requestedTask);
            db.SaveChanges();
        }
        public void RemoveRequstedTask(RequestedTask requstedTask)  
        {
            db.RequestedTasks.Remove(requstedTask);
            db.SaveChanges();
        }
    }
}
