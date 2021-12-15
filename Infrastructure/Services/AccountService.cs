using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServicesInterfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<int> RegisterUser(UserRegisterRequestModel model)
        {
            // make sure the email user entered does not exists in our database
            var dbUser = await _userRepository.GetUserByEmail(model.Email);

            if (dbUser != null)
                return 0;
            //  throw new Exception("Email already exists and please check"); 

            // continue with registration
            // create a unique salt
            var salt = GenerateSalt();

            // hash the passowrd with the salt created above
            var hashedPassword = GetHashedPassword(model.Password, salt);

            var user = new User
            {
                Email = model.Email,
                HashedPassword = hashedPassword,
                Salt = salt,
                DateOfBirth = model.DateOfBirth,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var creaedUser = await _userRepository.Add(user);
            return creaedUser.Id;
            // save to the database
            // reutn back
        }

        public async Task<UserLoginResponseModel> ValidateUser(LoginRequestModel model)
        {
            //check if the hashed password is correct
            var user = await _userRepository.GetUserByEmail(model.Email);
            if(user == null)
            {
                return null;
            }
            //hash the password the user entered with the salt from the database

            var hashedPassword = GetHashedPassword(model.Password, user.Salt);
            //compare the newly created hashpassword awith database password

            if(hashedPassword == user.HashedPassword)
            {
                var userLoginResponseModel = new UserLoginResponseModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    DateOfBirth = user.DateOfBirth,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
                return userLoginResponseModel;
            }
            return null;
        }

        private string GenerateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }

        private string GetHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                                                    password: password,
                                                                    salt: Convert.FromBase64String(salt),
                                                                    prf: KeyDerivationPrf.HMACSHA512,
                                                                    iterationCount: 10000,
                                                                    numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
