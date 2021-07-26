namespace MHC_Hospital_Redesign.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class category : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FaqCategories",
                c => new
                    {
                        FaqCategoryID = c.Int(nullable: false, identity: true),
                        CategoryDateAdded = c.DateTime(nullable: false),
                        CategoryName = c.String(),
                        CategoryDescription = c.String(),
                        CategoryColor = c.String(),
                    })
                .PrimaryKey(t => t.FaqCategoryID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FaqCategories");
        }
    }
}
