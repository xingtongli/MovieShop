import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { Login } from 'src/app/shared/models/login';
import { environment } from 'src/environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from 'src/app/shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private currentUserSubject = new BehaviorSubject<User>({} as User);
  public currentUser = this.currentUserSubject.asObservable();

  private isLoggedInSubject = new BehaviorSubject<boolean>(false);
  public isLoggedIn = this.isLoggedInSubject.asObservable();
  private jwtHelper = new JwtHelperService();

  constructor(private http:HttpClient) { }

  login(userLogin: Login): Observable<boolean>{
    //take email/password from login component and put it to api/account/login URL
    // if we get 200 DK status from API, email/password is correct, sow we get token from API
    // store the token in localstorage
    // return true to component

    return this.http.post(`${environment.apiBaseUrl}account/login`, userLogin).pipe(map(
      (response: any) => {
        if(response){
          // save the response token (JWT) to local storage
          localStorage.setItem('token', response.token);
          // create the observables so that other components can get notification when user successfully login
          // any component can subscribe to this observables to get the notification
          this.populateUserInfo();
          return true;
        }
        return false;
      }
    ))
  }

  populateUserInfo(){
    // get token from local storage
    var token = localStorage.getItem('token');

    if(token && !this.jwtHelper.isTokenExpired(token)){
      // decode the token, get the info, and put it inside user subject
      const decodedToekn = this.jwtHelper.decodeToken(token)

      // set current user data into Observable
      this.currentUserSubject.next(decodedToekn);

      //set it Authenticated to true
      this.isLoggedInSubject.next(true);
    }
   
  }

  logout(){
    // remove the token from local storage
    localStorage.removeItem('token');

    //reset the observanles to initial values
    this.currentUserSubject.next({} as User);
    this.isLoggedInSubject.next(false);
  }

  register(){
    // take the user registration info model
    // post it to api/account
    // if success, redirect to login route
  }

}
