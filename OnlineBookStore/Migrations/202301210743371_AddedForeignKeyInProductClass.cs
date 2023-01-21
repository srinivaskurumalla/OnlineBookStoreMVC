namespace OnlineBookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedForeignKeyInProductClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "BookTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "BookTypeId");
            AddForeignKey("dbo.Products", "BookTypeId", "dbo.BookTypes", "Id", cascadeDelete: true);
            DropColumn("dbo.Products", "BookType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "BookType", c => c.String(nullable: false, maxLength: 50, unicode: false));
            DropForeignKey("dbo.Products", "BookTypeId", "dbo.BookTypes");
            DropIndex("dbo.Products", new[] { "BookTypeId" });
            DropColumn("dbo.Products", "BookTypeId");
        }
    }
}
