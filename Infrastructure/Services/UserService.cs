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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> EditUserProfile(UserDetailsModel editProfile)
        {
            var curProfile = await _userRepository.GetUserDetails(editProfile.Id);
            bool edited = false;
            if(curProfile.FirstName != editProfile.FirstName || curProfile.LastName!=editProfile.LastName ||
                curProfile.DateOfBirth != editProfile.DateOfBirth || curProfile.PhoneNumber != editProfile.PhoneNumber)
            {
                edited = true;
                curProfile.FirstName = editProfile.FirstName;
                curProfile.LastName = editProfile.LastName;
                curProfile.DateOfBirth = editProfile.DateOfBirth;
                curProfile.PhoneNumber = editProfile.PhoneNumber;
            }
            await _userRepository.Update(curProfile);
            return edited;
        }

        public async Task<UserDetailsModel> GetUserDetails(int id)
        {
            var dbUser = await _userRepository.GetUserDetails(id);
            var userDetails = new UserDetailsModel
            {
                Id = id,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                DateOfBirth = dbUser.DateOfBirth,
                PhoneNumber = dbUser.PhoneNumber
            };
            return userDetails;
        }

        public async Task<List<MovieCardResponseModel>> GetUserFavoritedMovies(int id)
        {
            var favorite = await _userRepository.GetUserFavoriteMovies(id);
            var favoriteMovie = new List<MovieCardResponseModel>();
            foreach (var movie in favorite)
            {
                favoriteMovie.Add(new MovieCardResponseModel
                {
                    Id = movie.MovieId,
                    Title = movie.Movie.Title,
                    PosterUrl = movie.Movie.PosterUrl,
                });
            }
            return favoriteMovie;
        }

        public async Task<List<MovieCardResponseModel>> GetUserPurchasedMovies(int id)
        {
            var purchase = await _userRepository.GetUserPurchaseMovies(id);
            var purchaseMovie = new List<MovieCardResponseModel>();
            foreach(var movie in purchase)
            {
                purchaseMovie.Add(new MovieCardResponseModel
                {
                    Id = movie.MovieId,
                    Title = movie.Movie.Title,
                    PosterUrl = movie.Movie.PosterUrl,
                });
            }
            return purchaseMovie;
        }
    }
}
