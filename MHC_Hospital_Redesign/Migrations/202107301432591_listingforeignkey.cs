namespace MHC_Hospital_Redesign.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class listingforeignkey : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Listings", "DepartmentID");
            AddForeignKey("dbo.Listings", "DepartmentID", "dbo.Departments", "DId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Listings", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.Listings", new[] { "DepartmentID" });
        }
    }
}
