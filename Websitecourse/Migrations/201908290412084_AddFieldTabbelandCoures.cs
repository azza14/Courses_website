namespace Websitecourse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldTabbelandCoures : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fields",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FieldName = c.String(nullable: false),
                        FieldDescription = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseID = c.Int(nullable: false, identity: true),
                        CourseTitle = c.String(nullable: false),
                        CourseContent = c.String(nullable: false),
                        CourseDate = c.DateTime(nullable: false),
                        CourseDuration = c.String(nullable: false),
                        CourseImage = c.String(),
                        FieldId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseID)
                .ForeignKey("dbo.Fields", t => t.FieldId, cascadeDelete: true)
                .Index(t => t.FieldId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "FieldId", "dbo.Fields");
            DropIndex("dbo.Courses", new[] { "FieldId" });
            DropTable("dbo.Courses");
            DropTable("dbo.Fields");
        }
    }
}
