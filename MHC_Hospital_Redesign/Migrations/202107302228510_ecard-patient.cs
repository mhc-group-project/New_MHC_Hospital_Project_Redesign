namespace MHC_Hospital_Redesign.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ecardpatient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ecards", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Ecards", "UserId");
            AddForeignKey("dbo.Ecards", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ecards", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Ecards", new[] { "UserId" });
            DropColumn("dbo.Ecards", "UserId");
        }
    }
}
