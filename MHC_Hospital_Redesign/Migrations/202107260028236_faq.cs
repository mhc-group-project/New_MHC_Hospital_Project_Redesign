namespace MHC_Hospital_Redesign.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class faq : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Faqs",
                c => new
                    {
                        FaqID = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        FaqQuestions = c.String(),
                        FaqAnswers = c.String(),
                        FaqSort = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FaqID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Faqs");
        }
    }
}
