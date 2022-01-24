using System;
using System.Collections.Generic;

#nullable disable

namespace Sheldy.Models
{
    public partial class CategoryTask
    {
        public CategoryTask()
        {
            InverseParent = new HashSet<CategoryTask>();
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int Level { get; set; }

        public virtual CategoryTask Parent { get; set; }
        public virtual ICollection<CategoryTask> InverseParent { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
