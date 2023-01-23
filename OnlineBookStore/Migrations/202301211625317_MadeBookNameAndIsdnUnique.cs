namespace OnlineBookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeBookNameAndIsdnUnique : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Products", "BookName", unique: true);
            CreateIndex("dbo.Products", "ISBN", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Products", new[] { "ISBN" });
            DropIndex("dbo.Products", new[] { "BookName" });
        }
    }
}
