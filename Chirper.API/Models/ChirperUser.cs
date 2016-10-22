using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chirper.API.Models
{
    public class ChirperUser : IdentityUser
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string DisplayName { get; set; }
        //Setup Relationships
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ToDoListEntry> ToDoListEntries { get; set; }
    }
}