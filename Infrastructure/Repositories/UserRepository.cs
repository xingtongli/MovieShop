using ApplicationCore.Entities;
using ApplicationCore.Models;
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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }
        public async Task<User> GetUserDetails(int id)
        {
            var profile = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            return profile;
        }
        public async Task<List<Favorite>> GetUserFavoriteMovies(int id)
        {
            var favorite = await _dbContext.Favorites.Include(x => x.Movie).Where(x => x.UserId == id).ToListAsync();
            return favorite;
        }

        public async Task<List<Purchase>> GetUserPurchaseMovies(int id)
        {
            var purchase = await _dbContext.Purchases.Include(x => x.Movie).Where(x => x.UserId == id).ToListAsync();
            return purchase;
        }
    }
}
