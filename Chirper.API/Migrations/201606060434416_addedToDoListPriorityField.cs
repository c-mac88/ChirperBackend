namespace Chirper.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedToDoListPriorityField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoListEntries", "Priority", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoListEntries", "Priority");
        }
    }
}
