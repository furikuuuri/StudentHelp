using System;
using System.Collections.Generic;

#nullable disable

namespace Sheldy.Models
{
    public partial class Task
    {
        public Task()
        {
            AvailableTasks = new HashSet<AvailableTask>();
            RequestedTasks = new HashSet<RequestedTask>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public float Price { get; set; }
        public int CategoryTaskId { get; set; }

        public virtual CategoryTask CategoryTask { get; set; }
        public virtual ICollection<AvailableTask> AvailableTasks { get; set; }
        public virtual ICollection<RequestedTask> RequestedTasks { get; set; }
    }
}
