namespace MHC_Hospital_Redesign.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contactmethodaddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ContactMethod", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "ContactMethod");
        }
    }
}
