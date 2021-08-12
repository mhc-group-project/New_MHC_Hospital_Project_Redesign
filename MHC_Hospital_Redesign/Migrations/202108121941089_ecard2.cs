namespace MHC_Hospital_Redesign.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ecard2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ecards", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Ecards", new[] { "UserId" });
            DropColumn("dbo.Ecards", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ecards", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Ecards", "UserId");
            AddForeignKey("dbo.Ecards", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
