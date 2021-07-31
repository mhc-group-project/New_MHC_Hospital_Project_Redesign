namespace MHC_Hospital_Redesign.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FeedbackCategories",
                c => new
                    {
                        FeedbackCategoryID = c.Int(nullable: false, identity: true),
                        FeedbackCategoryName = c.String(),
                        FeedbackCategoryColor = c.String(),
                    })
                .PrimaryKey(t => t.FeedbackCategoryID);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        FeedbackId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        FeedbackContent = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                        FeedbackCategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FeedbackId)
                .ForeignKey("dbo.FeedbackCategories", t => t.FeedbackCategoryID, cascadeDelete: true)
                .Index(t => t.FeedbackCategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Feedbacks", "FeedbackCategoryID", "dbo.FeedbackCategories");
            DropIndex("dbo.Feedbacks", new[] { "FeedbackCategoryID" });
            DropTable("dbo.Feedbacks");
            DropTable("dbo.FeedbackCategories");
        }
    }
}
