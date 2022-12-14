using System;
using System.Collections.Generic;
using ToDoApp.Data.Abstract;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ToDoApp.Data.Entities
{
    public partial class Note: IBaseEntity
    {
        public Note()
        {
            NoteItem = new HashSet<NoteItem>();
        }

        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Title { get; set; }

        public virtual User Owner { get; set; }
        public virtual ICollection<NoteItem> NoteItem { get; set; }
    }
}
