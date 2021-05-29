namespace MovieZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyMovie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "NumberOfRatings", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "SumOfRatings", c => c.Int(nullable: false));
            DropColumn("dbo.Movies", "Rating");
            DropColumn("dbo.Movies", "NumberOfVotes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "NumberOfVotes", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "Rating", c => c.Double(nullable: false));
            DropColumn("dbo.Movies", "SumOfRatings");
            DropColumn("dbo.Movies", "NumberOfRatings");
        }
    }
}
