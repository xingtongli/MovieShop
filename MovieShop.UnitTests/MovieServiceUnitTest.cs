using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieShop.UnitTests
{
    [TestClass]
    public class MovieServiceUnitTest
    {
        /* Arrange: Initializes objects, creates mocks with arguments that are passed to the method under test and adds expectations
          Act: Invokes the method or property under test with the arranged parameters
          Assert: Verifies that the action of the method under test behaves as expected. */
        private MovieService _sut;
        private List<Movie> _movies;
        private Mock<IMovieRepository> _mockMovieRepository;

        [TestInitialize]
        //[OneTimeSetup] in nUnit
        public void OneTimeSetup()
        {
            _movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
                new Movie { Id = 2, Title = "Avatar", Budget = 1200000},
                new Movie{ Id = 3, Title = "Star Wars: The Force Awakens", Budget=1200000},
                new Movie{ Id = 4, Title = "Titanic", Budget = 1200000},
                new Movie{ Id=5, Title = "Inception", Budget = 1200000}
            };
        }

        [ClassInitialize]
        public void SetUp()
        {
            _mockMovieRepository = new Mock<IMovieRepository>();


            _mockMovieRepository.Setup(expression m => m.GetHighestGrossingMovies()).ReturnsAsync(_movies);
            _sut = new MovieService(_mockMovieRepository.Object);
        }

        [TestMethod]
        public async Task TestListOfHighestGrossingMoviesFromFakeData()
        {
            //Arrange 
            //mock objects, data, methods etc 
            _sut = new MovieService(new MockMovieRepository());
            //Act
            var movies = await _sut.GetHighestGrossingMovies();
            //check the actual output with expected data. 
            //AAA
            //Arrange, Act, Assert
            
            //Assert
            Assert.IsNotNull(movies);
        }
    }
    public class MockMovieRepository : IMovieRepository
    {
        Task<Movie> IRepository<Movie>.Add(Movie entity)
        {
            throw new System.NotImplementedException();
        }

        Task<Movie> IRepository<Movie>.Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> Get30HighestGrossingMovies()
        {
            var _movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
                new Movie { Id = 2, Title = "Avatar", Budget = 1200000},
                new Movie{ Id = 3, Title = "Star Wars: The Force Awakens", Budget=1200000},
                new Movie{ Id = 4, Title = "Titanic", Budget = 1200000},
                new Movie{ Id=5, Title = "Inception", Budget = 1200000}
            };
            return _movies;
        }

        Task<IEnumerable<Movie>> IMovieRepository.Get30HighestRatedMovies()
        {
            throw new System.NotImplementedException();
        }

        Task<List<Movie>> IRepository<Movie>.GetAll()
        {
            throw new System.NotImplementedException();
        }

        Task<IEnumerable<Movie>> IMovieRepository.GetAllMovies()
        {
            throw new System.NotImplementedException();
        }

        Task<IEnumerable<Movie>> IMovieRepository.GetByGenreId(int id)
        {
            throw new System.NotImplementedException();
        }

        Task<Movie> IRepository<Movie>.GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        Task<Movie> IRepository<Movie>.Update(Movie entity)
        {
            throw new System.NotImplementedException();
        }
    }
}