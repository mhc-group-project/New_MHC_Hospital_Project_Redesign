namespace MHC_Hospital_Redesign.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatedappointmentsanddeparments : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Payments", "CardHash");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "CardHash", c => c.String());
        }
    }
}
