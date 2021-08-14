namespace MHC_Hospital_Redesign.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateecard : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ecards", "SenderName", c => c.String(nullable: false));
            AlterColumn("dbo.Ecards", "PatientName", c => c.String(nullable: false));
            AlterColumn("dbo.Ecards", "Message", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ecards", "Message", c => c.String());
            AlterColumn("dbo.Ecards", "PatientName", c => c.String());
            AlterColumn("dbo.Ecards", "SenderName", c => c.String());
        }
    }
}
