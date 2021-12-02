﻿using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServicesInterfaces
{
    public interface IMovieService
    {
        // Expose the methods thgat are required by the client/views
        IEnumerable<MovieCardResponseModel> GetHighestGrossingMovies();

    }
}
