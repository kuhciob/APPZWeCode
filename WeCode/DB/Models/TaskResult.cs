using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WeCode
{
    public partial class TaskResult
    {
        public TaskResult()
        {
            ActualResults = new HashSet<ActualResult>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int TaskResultId { get; set; }
        public int? TaskId { get; set; }
        public int? SubmittedBy { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? Score { get; set; }

        public virtual User SubmittedByNavigation { get; set; }
        public virtual Task Task { get; set; }
        public virtual ICollection<ActualResult> ActualResults { get; set; }
    }
}
