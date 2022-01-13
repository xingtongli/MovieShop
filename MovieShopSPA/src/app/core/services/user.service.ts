import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MovieCard } from 'src/app/shared/models/movieCard';
import { User } from 'src/app/shared/models/user';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  // get user details by id
  getUserDetail(id: number): Observable<User[]>{

    return this.http.get<User[]>(`${environment.apiBaseUrl}Account/${id}`);
  }

  // get movies purchased by user
  getUserPurchases(id: number): Observable<MovieCard[]>{
    //call API methods that returns movie details
    //token with http header
    //Angular we have calss called HttpHeader
    var token = localStorage.getItem('token');
    //headers.set('Anthorization','Bearer ${token}')
    return this.http.get<MovieCard[]>(`${environment.apiBaseUrl}User/${id}/purchases`);
  }

  // get favorites by user 
  getUserFavorties(id: number): Observable<MovieCard[]>{

    return this.http.get<MovieCard[]>(`${environment.apiBaseUrl}User/${id}/favorite`);
  }
}
