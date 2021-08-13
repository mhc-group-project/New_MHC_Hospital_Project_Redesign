namespace MHC_Hospital_Redesign.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class template2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Templates", "TemplateHasStyle", c => c.Boolean(nullable: false));
            AddColumn("dbo.Templates", "TemplateStyleExtension", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Templates", "TemplateStyleExtension");
            DropColumn("dbo.Templates", "TemplateHasStyle");
        }
    }
}
