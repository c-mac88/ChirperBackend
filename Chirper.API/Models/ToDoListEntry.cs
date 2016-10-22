using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chirper.API.Models
{
    public class ToDoListEntry
    {
        //Primary Key
        public int ToDoListEntryId { get; set; }
        //Foreign key
        public string UserId { get; set; }
        //Fields relevant to Post
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedTime { get; set; }
        public string Text { get; set; }
        public bool Completed { get; set; }
        public int Priority { get; set; }


        //Relationship fields
        public virtual ChirperUser User { get; set; }
    }
}