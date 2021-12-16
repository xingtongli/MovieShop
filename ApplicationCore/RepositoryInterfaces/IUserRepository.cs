using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserDetails(int id);
        Task<List<Purchase>> GetUserPurchaseMovies(int id);
        Task<List<Favorite>> GetUserFavoriteMovies(int id);  
    }
}
