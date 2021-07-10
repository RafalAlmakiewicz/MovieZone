using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using MovieZone.Core.Domain;

namespace MovieZone.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<MovieSubmission> MovieSubmissions { get; set; }
        public DbSet<DirectorSubmission> DirectorSubmissions { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        //public override Tkey Id {get; set;}
        

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}