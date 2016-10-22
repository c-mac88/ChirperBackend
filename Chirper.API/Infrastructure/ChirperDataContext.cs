using Chirper.API.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Chirper.API.Infrastructure
{
    public class ChirperDataContext : IdentityDbContext
    {
        public ChirperDataContext() : base("Chirper")
        {
        }

        public IDbSet<Post> Posts { get; set; }
        public IDbSet<Comment> Comments { get; set; }
        public IDbSet<ToDoListEntry> ToDoListEntries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithRequired(c => c.post)
                .HasForeignKey(c => c.PostId);

            //Configure one to many relationship user and post
            modelBuilder.Entity<ChirperUser>()
                .HasMany(u => u.Posts)
                .WithRequired(p => p.User)
                .HasForeignKey(p => p.UserId);


            //Configure one to many relationship user and post
            modelBuilder.Entity<ChirperUser>()
                .HasMany(u => u.ToDoListEntries)
                .WithRequired(t => t.User)
                .HasForeignKey(t => t.UserId);

            //Configure one to many user and comment
            modelBuilder.Entity<ChirperUser>()
                .HasMany(u => u.Comments)
                .WithRequired(c => c.User)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<Chirper.API.Models.ChirperUser> IdentityUsers { get; set; }
    }
}