import { HttpClient } from '@angular/common/http';
import { AccountService } from './_services/account.service';
import { Component, OnInit } from '@angular/core';
import { AnyARecord } from 'dns';
import { UsersService } from './_services/users.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  users:any;

  constructor(private http: HttpClient, private usersService: UsersService) {

  }

  ngOnInit(): void {

  }
}
