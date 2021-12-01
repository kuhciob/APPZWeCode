using System;
using System.Collections.Generic;

#nullable disable

namespace WeCode
{
    public partial class User
    {
        public User()
        {
            TaskResults = new HashSet<TaskResult>();
            Tasks = new HashSet<Task>();
        }

        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Password { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<TaskResult> TaskResults { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
