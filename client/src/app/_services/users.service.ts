import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  baseApi = "https://localhost:5001/api/users/";
  constructor(private http: HttpClient) { }

  getUsers(){
    return this.http.get(this.baseApi);
  }

  getUser(id: number){
    return this.http.get(this.baseApi + id);
  }
}
