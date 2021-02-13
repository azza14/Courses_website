namespace Websitecourse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplyToCourse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplyToCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        ApplyDate = c.DateTime(nullable: false),
                        CourseId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.CourseId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplyToCourses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplyToCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.ApplyToCourses", new[] { "UserId" });
            DropIndex("dbo.ApplyToCourses", new[] { "CourseId" });
            DropTable("dbo.ApplyToCourses");
        }
    }
}
