namespace MHC_Hospital_Redesign.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedValidationFaq : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FaqCategories", "CategoryName", c => c.String(nullable: false));
            AlterColumn("dbo.FaqCategories", "CategoryDescription", c => c.String(nullable: false));
            AlterColumn("dbo.FaqCategories", "CategoryColor", c => c.String(nullable: false));
            AlterColumn("dbo.Faqs", "FaqQuestions", c => c.String(nullable: false));
            AlterColumn("dbo.Faqs", "FaqAnswers", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Faqs", "FaqAnswers", c => c.String());
            AlterColumn("dbo.Faqs", "FaqQuestions", c => c.String());
            AlterColumn("dbo.FaqCategories", "CategoryColor", c => c.String());
            AlterColumn("dbo.FaqCategories", "CategoryDescription", c => c.String());
            AlterColumn("dbo.FaqCategories", "CategoryName", c => c.String());
        }
    }
}
