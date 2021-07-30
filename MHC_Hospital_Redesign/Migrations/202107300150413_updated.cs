namespace MHC_Hospital_Redesign.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        AId = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Message = c.String(),
                        DateTime = c.String(),
                        Status = c.String(),
                        PatientId = c.String(maxLength: 128),
                        DoctorId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AId)
                .ForeignKey("dbo.AspNetUsers", t => t.DoctorId)
                .ForeignKey("dbo.AspNetUsers", t => t.PatientId)
                .Index(t => t.PatientId)
                .Index(t => t.DoctorId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ContactMethod = c.String(),
                        Address = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Listings",
                c => new
                    {
                        ListID = c.Int(nullable: false, identity: true),
                        ListTitle = c.String(),
                        ListDate = c.DateTime(nullable: false),
                        ListDescription = c.String(),
                        ListRequirements = c.String(),
                        ListLocation = c.String(),
                        DepartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ListID);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DId = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.DId);
            
            CreateTable(
                "dbo.Ecards",
                c => new
                    {
                        EcardId = c.Int(nullable: false, identity: true),
                        SenderName = c.String(),
                        PatientName = c.String(),
                        Message = c.String(),
                        TemplateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EcardId)
                .ForeignKey("dbo.Templates", t => t.TemplateId, cascadeDelete: true)
                .Index(t => t.TemplateId);
            
            CreateTable(
                "dbo.Templates",
                c => new
                    {
                        TemplateId = c.Int(nullable: false, identity: true),
                        TemplateName = c.String(),
                        TemplateHasPic = c.Boolean(nullable: false),
                        TemplatePicExtension = c.String(),
                        TemplateStyle = c.String(),
                    })
                .PrimaryKey(t => t.TemplateId);
            
            CreateTable(
                "dbo.FaqCategories",
                c => new
                    {
                        FaqCategoryID = c.Int(nullable: false, identity: true),
                        CategoryDateAdded = c.DateTime(nullable: false),
                        CategoryName = c.String(),
                        CategoryDescription = c.String(),
                        CategoryColor = c.String(),
                    })
                .PrimaryKey(t => t.FaqCategoryID);
            
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
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceID = c.Int(nullable: false, identity: true),
                        InvoiceNumber = c.Int(nullable: false),
                        InvoiceDate = c.DateTime(nullable: false),
                        Amount = c.Int(nullable: false),
                        Currency = c.String(),
                    })
                .PrimaryKey(t => t.InvoiceID);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentID = c.Int(nullable: false, identity: true),
                        NameOnCard = c.String(),
                        CardHash = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        PostalCode = c.String(),
                        Province = c.String(),
                        Country = c.String(),
                        InvoiceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentID)
                .ForeignKey("dbo.Invoices", t => t.InvoiceID, cascadeDelete: true)
                .Index(t => t.InvoiceID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ListingApplicationUsers",
                c => new
                    {
                        Listing_ListID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Listing_ListID, t.ApplicationUser_Id })
                .ForeignKey("dbo.Listings", t => t.Listing_ListID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Listing_ListID)
                .Index(t => t.ApplicationUser_Id);
            
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
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Payments", "InvoiceID", "dbo.Invoices");
            DropForeignKey("dbo.FaqFaqCategories", "FaqCategory_FaqCategoryID", "dbo.FaqCategories");
            DropForeignKey("dbo.FaqFaqCategories", "Faq_FaqID", "dbo.Faqs");
            DropForeignKey("dbo.Ecards", "TemplateId", "dbo.Templates");
            DropForeignKey("dbo.Appointments", "PatientId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Appointments", "DoctorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ListingApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ListingApplicationUsers", "Listing_ListID", "dbo.Listings");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.FaqFaqCategories", new[] { "FaqCategory_FaqCategoryID" });
            DropIndex("dbo.FaqFaqCategories", new[] { "Faq_FaqID" });
            DropIndex("dbo.ListingApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ListingApplicationUsers", new[] { "Listing_ListID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Payments", new[] { "InvoiceID" });
            DropIndex("dbo.Ecards", new[] { "TemplateId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Appointments", new[] { "DoctorId" });
            DropIndex("dbo.Appointments", new[] { "PatientId" });
            DropTable("dbo.FaqFaqCategories");
            DropTable("dbo.ListingApplicationUsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Payments");
            DropTable("dbo.Invoices");
            DropTable("dbo.Faqs");
            DropTable("dbo.FaqCategories");
            DropTable("dbo.Templates");
            DropTable("dbo.Ecards");
            DropTable("dbo.Departments");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Listings");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Appointments");
        }
    }
}
