﻿using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServicesInterfaces
{
    public interface IAccountService
    {
        Task<int> RegisterUser(UserRegisterRequestModel model);
        Task<UserLoginResponseModel> ValidateUser(LoginRequestModel model);
    }
}
