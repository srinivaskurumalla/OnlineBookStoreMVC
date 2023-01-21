namespace OnlineBookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializedProductClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookName = c.String(nullable: false, maxLength: 50, unicode: false),
                        ISBN = c.String(nullable: false, maxLength: 50, unicode: false),
                        AuthorName = c.String(nullable: false, maxLength: 50, unicode: false),
                        Publisher = c.String(nullable: false, maxLength: 50, unicode: false),
                        BookType = c.String(nullable: false, maxLength: 50, unicode: false),
                        Description = c.String(maxLength: 50, unicode: false),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        PublishedDate = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}
