namespace MovieZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMovieRatingAndReviewModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "MovieId", "dbo.Movies");
            DropIndex("dbo.Reviews", new[] { "MovieId" });
            AddColumn("dbo.Movies", "ReleaseYear", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "Rating", c => c.Double(nullable: false));
            AddColumn("dbo.Movies", "NumberOfVotes", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "ReviewId", c => c.Int());
            AddColumn("dbo.Reviews", "DateAdded", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Reviews", "Body", c => c.String(nullable: false));
            CreateIndex("dbo.Ratings", "ReviewId");
            AddForeignKey("dbo.Ratings", "ReviewId", "dbo.Reviews", "Id");
            DropColumn("dbo.Movies", "ReleaseDate");
            DropColumn("dbo.Reviews", "MovieId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reviews", "MovieId", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "ReleaseDate", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Ratings", "ReviewId", "dbo.Reviews");
            DropIndex("dbo.Ratings", new[] { "ReviewId" });
            AlterColumn("dbo.Reviews", "Body", c => c.String());
            DropColumn("dbo.Reviews", "DateAdded");
            DropColumn("dbo.Ratings", "ReviewId");
            DropColumn("dbo.Movies", "NumberOfVotes");
            DropColumn("dbo.Movies", "Rating");
            DropColumn("dbo.Movies", "ReleaseYear");
            CreateIndex("dbo.Reviews", "MovieId");
            AddForeignKey("dbo.Reviews", "MovieId", "dbo.Movies", "Id", cascadeDelete: true);
        }
    }
}
