namespace MHC_Hospital_Redesign.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class faqcategorybridging : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FaqFaqCategories",
                c => new
                    {
                        Faq_FaqID = c.Int(nullable: false),
                        FaqCategory_FaqCategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Faq_FaqID, t.FaqCategory_FaqCategoryID })
                .ForeignKey("dbo.Faqs", t => t.Faq_FaqID, cascadeDelete: true)
                .ForeignKey("dbo.FaqCategories", t => t.FaqCategory_FaqCategoryID, cascadeDelete: true)
                .Index(t => t.Faq_FaqID)
                .Index(t => t.FaqCategory_FaqCategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FaqFaqCategories", "FaqCategory_FaqCategoryID", "dbo.FaqCategories");
            DropForeignKey("dbo.FaqFaqCategories", "Faq_FaqID", "dbo.Faqs");
            DropIndex("dbo.FaqFaqCategories", new[] { "FaqCategory_FaqCategoryID" });
            DropIndex("dbo.FaqFaqCategories", new[] { "Faq_FaqID" });
            DropTable("dbo.FaqFaqCategories");
        }
    }
}
