using MovieZone.Core.Interfaces;
using MovieZone.Persistance.Repositories;
using MovieZone.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieZone.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Movies = new MovieRepository(context);
            //Directors = new DirectorRepository(context);
        }
        public IMovieRepository Movies { get; private set; }
        //public IDirectorRepository Directors { get; private set; }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}