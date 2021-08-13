namespace MHC_Hospital_Redesign.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updateddata : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Appointments", "Status", c => c.Int(nullable: false));
            CreateIndex("dbo.Departments", "UserId");
            AddForeignKey("dbo.Departments", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Departments", new[] { "UserId" });
            AlterColumn("dbo.Appointments", "Status", c => c.String(nullable: false));
            DropColumn("dbo.Departments", "UserId");
        }
    }
}
