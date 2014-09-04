namespace Phone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageUrlTocellPhone : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CellPhones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Status = c.Boolean(nullable: false),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            Sql("insert into CellPhones (Title,Status,ImageUrl) values ('Apple',1,newID())");
            Sql("insert into CellPhones (Title,Status,ImageUrl) values ('Samsung',1,newID())");
            Sql("insert into CellPhones (Title,Status,ImageUrl) values ('HTC',1,newID())");
            Sql("insert into CellPhones (Title,Status,ImageUrl) values ('Sony',1,newID())");
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CellPhones");
        }
    }
}
