using System;
using System.Collections.Generic;

#nullable disable

namespace Sheldy.Models
{
    public partial class User
    {
        public User()
        {
            AvailableTasks = new HashSet<AvailableTask>();
            RequestedTasks = new HashSet<RequestedTask>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<AvailableTask> AvailableTasks { get; set; }
        public virtual ICollection<RequestedTask> RequestedTasks { get; set; }
    }
}
