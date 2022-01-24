using System;
using System.Collections.Generic;

#nullable disable

namespace Sheldy.Models
{
    public partial class AvailableTask
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public DateTime Time { get; set; }

        public virtual Task Task { get; set; }
        public virtual User User { get; set; }
    }
}
