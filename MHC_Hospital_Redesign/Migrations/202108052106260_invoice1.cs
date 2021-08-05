namespace MHC_Hospital_Redesign.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoice1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Appointments", "Subject", c => c.String(nullable: false));
            AlterColumn("dbo.Appointments", "Message", c => c.String(nullable: false));
            AlterColumn("dbo.Appointments", "DateTime", c => c.String(nullable: false));
            AlterColumn("dbo.Appointments", "Status", c => c.String(nullable: false));
            AlterColumn("dbo.Departments", "DepartmentName", c => c.String(nullable: false));
            AlterColumn("dbo.Departments", "PhoneNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Departments", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Departments", "DepartmentName", c => c.String());
            AlterColumn("dbo.Appointments", "Status", c => c.String());
            AlterColumn("dbo.Appointments", "DateTime", c => c.String());
            AlterColumn("dbo.Appointments", "Message", c => c.String());
            AlterColumn("dbo.Appointments", "Subject", c => c.String());
        }
    }
}
