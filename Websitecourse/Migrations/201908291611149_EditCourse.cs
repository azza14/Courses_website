namespace Websitecourse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditCourse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Courses", "UserID");
            AddForeignKey("dbo.Courses", "UserID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Courses", new[] { "UserID" });
            DropColumn("dbo.Courses", "UserID");
        }
    }
}
