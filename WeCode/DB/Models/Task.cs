using System;
using System.Collections.Generic;

#nullable disable

namespace WeCode
{
    public partial class Task
    {
        public Task()
        {
            ExpectedResults = new HashSet<ExpectedResult>();
            TaskResults = new HashSet<TaskResult>();
        }

        public int TaskId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int? Difficulty { get; set; }
        public int? CreatedBy { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual ICollection<ExpectedResult> ExpectedResults { get; set; }
        public virtual ICollection<TaskResult> TaskResults { get; set; }
    }
}
