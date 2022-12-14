import { User } from './../_,models/user';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators'

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseApiUrl = "https://localhost:5001/api/account/";
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http:HttpClient) { }

  register(model:any){
    return this.http.post(this.baseApiUrl + "register", model).pipe(
      map((user: User) => {
        this.currentUserSource.next(user);
      })
    )
  }

  login(model:any){
    return this.http.post(this.baseApiUrl + "login", model).pipe(
      map((user: User) => {
        console.log(user);
        this.setNextUser(user);
      })
    )
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }


  setNextUser(user: User){
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }
}
