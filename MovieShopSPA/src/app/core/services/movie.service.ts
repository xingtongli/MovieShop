import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MovieCard } from 'src/app/shared/models/movieCard';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { MovieDetails } from 'src/app/shared/models/movieDetails';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  constructor(private http: HttpClient) { }

  //home component will call this method
  //array of movie card model
  //observables, RXJS
  getTopGrossingMovies(): Observable<MovieCard[]>{
  //call API api/Movies/toprevenue
  //HttpClient calss comes from HttpClientModule in angular
  return this.http.get<MovieCard[]>(`${environment.apiBaseUrl}movies/toprevenue`);
  }
  getMovieDetails(id:number){
    //call api to get movie details, create the model based on json data and return the model
    return this.http.get<MovieDetails>(`${environment.apiBaseUrl}movies/details/${id}`);
  }
}
//dependency injection
//var movies = dbcontext.movie.where(x => x.revenue>1000).tolist();
