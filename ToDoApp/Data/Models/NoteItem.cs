using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ToDoApp.Data.Models
{
    public partial class NoteItem
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public string Content { get; set; }

        public virtual Note Note { get; set; }
    }
}
