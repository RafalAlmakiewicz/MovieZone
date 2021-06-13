namespace MovieZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMovieSubmissionsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovieSubmissions",
                c => new
                    {
                        SubmissionId = c.Int(nullable: false, identity: true),
                        MovieId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 255),
                        ReleaseYear = c.Int(nullable: false),
                        DirectorId = c.Int(nullable: false),
                        DurationInMinutes = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.SubmissionId)
                .ForeignKey("dbo.Directors", t => t.DirectorId, cascadeDelete: true)
                .Index(t => t.DirectorId);
            
            CreateTable(
                "dbo.MovieSubmissionGenres",
                c => new
                    {
                        MovieSubmission_SubmissionId = c.Int(nullable: false),
                        Genre_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieSubmission_SubmissionId, t.Genre_Id })
                .ForeignKey("dbo.MovieSubmissions", t => t.MovieSubmission_SubmissionId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .Index(t => t.MovieSubmission_SubmissionId)
                .Index(t => t.Genre_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieSubmissionGenres", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.MovieSubmissionGenres", "MovieSubmission_SubmissionId", "dbo.MovieSubmissions");
            DropForeignKey("dbo.MovieSubmissions", "DirectorId", "dbo.Directors");
            DropIndex("dbo.MovieSubmissionGenres", new[] { "Genre_Id" });
            DropIndex("dbo.MovieSubmissionGenres", new[] { "MovieSubmission_SubmissionId" });
            DropIndex("dbo.MovieSubmissions", new[] { "DirectorId" });
            DropTable("dbo.MovieSubmissionGenres");
            DropTable("dbo.MovieSubmissions");
        }
    }
}
