namespace MHC_Hospital_Redesign.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class listingsusers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUserListings",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Listing_ListID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Listing_ListID })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Listings", t => t.Listing_ListID, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Listing_ListID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserListings", "Listing_ListID", "dbo.Listings");
            DropForeignKey("dbo.ApplicationUserListings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserListings", new[] { "Listing_ListID" });
            DropIndex("dbo.ApplicationUserListings", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserListings");
        }
    }
}
