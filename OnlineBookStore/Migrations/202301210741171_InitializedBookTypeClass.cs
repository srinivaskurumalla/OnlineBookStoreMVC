namespace OnlineBookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializedBookTypeClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookTypeName = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BookTypes");
        }
    }
}
