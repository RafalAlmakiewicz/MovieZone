namespace MovieZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDirectorSubmissionsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DirectorSubmissions",
                c => new
                    {
                        SubmissionId = c.Int(nullable: false, identity: true),
                        DirectorId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Bio = c.String(),
                    })
                .PrimaryKey(t => t.SubmissionId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DirectorSubmissions");
        }
    }
}
