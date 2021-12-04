using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


#nullable disable

namespace WeCode
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int RoleId { get; set; }
        public string Name { get; set; }
        //[System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
    public static class RoleType 
    {
        public const string Admin = "admin";
        public const string Student = "student";
    }
}
