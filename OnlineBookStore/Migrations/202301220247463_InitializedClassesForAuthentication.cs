namespace OnlineBookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializedClassesForAuthentication : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRolesMappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRolesMappings", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRolesMappings", "RoleId", "dbo.Roles");
            DropIndex("dbo.UserRolesMappings", new[] { "RoleId" });
            DropIndex("dbo.UserRolesMappings", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.UserRolesMappings");
            DropTable("dbo.Roles");
        }
    }
}
