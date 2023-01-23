namespace OnlineBookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUniqueConstraints : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.UserRolesMappings", new[] { "RoleId" });
            CreateIndex("dbo.UserRolesMappings", "RoleId", unique: true, name: "IX_UserId_RoleId");
            CreateIndex("dbo.Users", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Name" });
            DropIndex("dbo.UserRolesMappings", "IX_UserId_RoleId");
            CreateIndex("dbo.UserRolesMappings", "RoleId");
        }
    }
}
