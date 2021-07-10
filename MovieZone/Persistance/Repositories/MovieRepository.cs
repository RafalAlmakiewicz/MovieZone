using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieZone.Core.Domain;
using MovieZone.Core.Interfaces;

namespace MovieZone.Persistance.Repositories
{
    public class MovieRepository : Repository<Movie> ,IMovieRepository
    {
        public MovieRepository(ApplicationDbContext context) : base(context) { }


        public IEnumerable<Movie> GetTop10HighestRatedMovies()
        {
            throw new NotImplementedException();
        }

        public ApplicationDbContext Context => context as ApplicationDbContext;
    }
}