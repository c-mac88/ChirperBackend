using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chirper.API.Models
{
    public class Comment
    {
        //Primary Key
        public int CommentID { get; set; }
        //Foreign Key
        public int PostId { get; set; }
        public string UserId { get; set; }

        //Fields relevant to Comment
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedTime { get; set; }
        public string Text { get; set; }

        //relationship fields
        public virtual Post post { get; set; }
        public virtual ChirperUser User { get; set; }

    }
}