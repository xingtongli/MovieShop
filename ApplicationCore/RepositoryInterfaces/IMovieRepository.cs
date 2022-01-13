using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task <IEnumerable<Movie>> Get30HighestGrossingMovies();
        Task<IEnumerable<Movie>> Get30HighestRatedMovies();
        Task<IEnumerable<Movie>> GetAllMovies();
        Task<IEnumerable<Movie>> GetByGenreId(int id);
        
    }
}
