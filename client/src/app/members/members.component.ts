import { AccountService } from './../_services/account.service';
import { User } from './../_,models/user';
import { UsersService } from './../_services/users.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css']
})
export class MembersComponent implements OnInit {

  users:User;

  constructor(private usersService: UsersService, private accountService: AccountService) {

  }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers(){
    this.usersService.getUsers().subscribe((response: User) => {
      this.users = response;
    }, error => {
      console.log(error);
    })
  }
}
