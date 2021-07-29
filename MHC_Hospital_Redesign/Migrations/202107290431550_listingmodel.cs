namespace MHC_Hospital_Redesign.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class listingmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Listings",
                c => new
                    {
                        ListID = c.Int(nullable: false, identity: true),
                        ListTitle = c.String(),
                        ListDate = c.DateTime(nullable: false),
                        ListDescription = c.String(),
                        ListRequirements = c.String(),
                        ListLocation = c.String(),
                        DepartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ListID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Listings");
        }
    }
}
