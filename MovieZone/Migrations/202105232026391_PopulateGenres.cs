namespace MovieZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenres : DbMigration
    {
        public override void Up()
        {
            //Sql("insert into Genres (id, name) values (1,'Comedy')");
            //Sql("insert into Genres (id, name) values (2,'Horror')");
            //Sql("insert into Genres (id, name) values (3,'Drama')");
            //Sql("insert into Genres (id, name) values (4,'Action')");
            //Sql("insert into Genres (id, name) values (5,'Sci-Fi')");
            //Sql("insert into Genres (id, name) values (6,'Fantasy')");
            //Sql("insert into Genres (id, name) values (7,'Thriller')");
            //Sql("insert into Genres (id, name) values (8,'War')");
            //Sql("insert into Genres (id, name) values (9,'Romance')");
            //Sql("insert into Genres (id, name) values (10,'Documentary')");
            //Sql("insert into Genres (id, name) values (11,'Biography')");
            //Sql("insert into Genres (id, name) values (12,'History')");
            //Sql("insert into Genres (id, name) values (13,'Family')");
            //Sql("insert into Genres (id, name) values (14,'Adventure')");
            //Sql("insert into Genres (id, name) values (15,'Mystery')");
            //Sql("insert into Genres (id, name) values (16,'Animation')");
            //Sql("insert into Genres (id, name) values (17,'Musical')");

            Sql("insert into Genres (name) values ('Comedy')");
            Sql("insert into Genres (name) values ('Horror')");
            Sql("insert into Genres (name) values ('Drama')");
            Sql("insert into Genres (name) values ('Action')");
            Sql("insert into Genres (name) values ('Sci-Fi')");
            Sql("insert into Genres (name) values ('Fantasy')");
            Sql("insert into Genres (name) values ('Thriller')");
            Sql("insert into Genres (name) values ('War')");
            Sql("insert into Genres (name) values ('Romance')");
            Sql("insert into Genres (name) values ('Documentary')");
            Sql("insert into Genres (name) values ('Biography')");
            Sql("insert into Genres (name) values ('History')");
            Sql("insert into Genres (name) values ('Family')");
            Sql("insert into Genres (name) values ('Adventure')");
            Sql("insert into Genres (name) values ('Mystery')");
            Sql("insert into Genres (name) values ('Animation')");
            Sql("insert into Genres (name) values ('Musical')");

        }
        
        public override void Down()
        {
        }
    }
}
