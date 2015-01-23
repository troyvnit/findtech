namespace FindTech.Entities.AuthenticationMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FindTechRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.FindTechUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.FindTechRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.FindTechUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.FindTechUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
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
                "dbo.FindTechUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FindTechUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.FindTechUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.FindTechUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FindTechUserRoles", "UserId", "dbo.FindTechUsers");
            DropForeignKey("dbo.FindTechUserLogins", "UserId", "dbo.FindTechUsers");
            DropForeignKey("dbo.FindTechUserClaims", "UserId", "dbo.FindTechUsers");
            DropForeignKey("dbo.FindTechUserRoles", "RoleId", "dbo.FindTechRoles");
            DropIndex("dbo.FindTechUserLogins", new[] { "UserId" });
            DropIndex("dbo.FindTechUserClaims", new[] { "UserId" });
            DropIndex("dbo.FindTechUsers", "UserNameIndex");
            DropIndex("dbo.FindTechUserRoles", new[] { "RoleId" });
            DropIndex("dbo.FindTechUserRoles", new[] { "UserId" });
            DropIndex("dbo.FindTechRoles", "RoleNameIndex");
            DropTable("dbo.FindTechUserLogins");
            DropTable("dbo.FindTechUserClaims");
            DropTable("dbo.FindTechUsers");
            DropTable("dbo.FindTechUserRoles");
            DropTable("dbo.FindTechRoles");
        }
    }
}
