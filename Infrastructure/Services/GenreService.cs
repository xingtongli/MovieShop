using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMemoryCache _memoryCache;
        private static readonly string _genresCacheKey = "genres";
        private static readonly TimeSpan DefaultCacheDuration = TimeSpan.FromDays(7);
        public GenreService(IGenreRepository genreRepository, IMemoryCache memoryCache)
        {
            _genreRepository = genreRepository;
            _memoryCache = memoryCache;
        }
        public async Task<List<GenreModel>> GetAllGenres()
        {
            // this is the database call
            // first check the cache if the genres are already in the cache 
            // if the genres already present in the memory then we dont' need to go to database
            // just read the genres from the cache

            // if the genres is not present in the cache => then go to database and get the genres
            // then store them in the cache

            // make sure the cache is not expired then get the data
            // when we update/create any new genre then we call the cache and delete that from cache

            var genresFromCache = await _memoryCache.GetOrCreateAsync(_genresCacheKey, CacheFactory);
            if (genresFromCache != null) 
            {
                return genresFromCache.OrderBy(o => o.Name).ToList();
            }
            else
            {
                var genres = await _genreRepository.GetAll();

                var genreModel = new List<GenreModel>();
                foreach (var genre in genres)
                {
                    genreModel.Add(new GenreModel { Id = genre.Id, Name = genre.Name });
                }
                return genreModel;
            }
        }
        private async Task<IEnumerable<GenreModel>> CacheFactory(ICacheEntry entry)
        {
            entry.SlidingExpiration = DefaultCacheDuration;
            var genres = await _genreRepository.GetAll();
            var genreModel = new List<GenreModel>();
            foreach (var genre in genres)
            {
                genreModel.Add(new GenreModel { Id = genre.Id, Name = genre.Name });
            }
            return genreModel;
        }
    }
}
