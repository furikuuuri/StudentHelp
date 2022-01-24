using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Sheldy.Models;
namespace Sheldy.Tasks
{
    public class TaskList
    {
        SheldyContext db;
        public TaskList()
        {
            db = new SheldyContext();
        }
        public void AddTask(Task task)
        {
            db.Tasks.Add(task);
            db.SaveChanges();
        }
        public IEnumerable<Task> getTasks(int categoryId)
        {
            return db.Tasks.Where(p => p.CategoryTaskId == categoryId);
        }
        public IEnumerable<AvailableTask> getMyTasks(int userId)
        {
            return db.AvailableTasks.Include(p => p.Task).Where(p => p.UserId == userId);
        }

    }
}
