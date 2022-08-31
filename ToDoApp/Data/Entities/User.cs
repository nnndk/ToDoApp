using System;
using System.Collections.Generic;
using ToDoApp.Data.Abstract;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ToDoApp.Data.Entities
{
    public partial class User: IBaseEntity
    {
        public User()
        {
            Note = new HashSet<Note>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Note> Note { get; set; }
    }
}
