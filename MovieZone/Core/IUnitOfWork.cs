using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieZone.Core.Interfaces;

namespace MovieZone.Core
{
    interface IUnitOfWork : IDisposable
    {
        int Complete();

        IMovieRepository Movies { get; }
        //IDirectorRepository DirectorRepository { get; }
    }
}
