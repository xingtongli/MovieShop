using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
            
        }
        public async Task <IEnumerable<Movie>> Get30HighestGrossingMovies()
        {
            // we need to go to database and get the movies using Dapper or EF Core
            //access the dbcontext object and dbset of movies object to query the movie table
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        } 
        public override async Task<Movie> GetById(int id)
        {
            var movieDetails = _dbContext.Movies.Include(m => m.CastsOfMovie).ThenInclude(m => m.Cast)
                .Include(m => m.GenresOfMovie).ThenInclude(m => m.Genre).Include(m => m.Trailers)
                .FirstOrDefault(m => m.Id == id);

            if(movieDetails == null) return null;
            var rating = _dbContext.Reviews.Where(r => r.MovieId == id).DefaultIfEmpty()
                     .Average(r => r == null ? 0 : r.Rating);
            movieDetails.Rating = rating;
            return movieDetails;
        }
    }
}
