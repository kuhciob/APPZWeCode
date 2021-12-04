using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [System.Text.Json.Serialization.JsonIgnore]
        public int TaskId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int? Difficulty { get; set; }
        public int? CreatedBy { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        //[System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<ExpectedResult> ExpectedResults { get; set; }
        //[System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<TaskResult> TaskResults { get; set; }
    }
}
