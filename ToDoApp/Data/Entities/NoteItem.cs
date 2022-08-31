using System;
using System.Collections.Generic;
using ToDoApp.Data.Abstract;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ToDoApp.Data.Entities
{
    public partial class NoteItem: IBaseEntity
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public string Content { get; set; }

        public virtual Note Note { get; set; }
    }
}
