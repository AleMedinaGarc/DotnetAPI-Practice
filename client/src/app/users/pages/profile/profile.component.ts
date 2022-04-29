import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { UserService } from '../../users.service';
@Component({
  selector: 'profile-component',
  templateUrl: './profile.component.html', // Manages Component's HTML
  styleUrls: ['./profile.component.scss'], // Handles Component's styling
})
export class ProfileComponent implements OnInit {
  userdata: any;
  items: MenuItem[] | any;

  constructor(private userService: UserService) {}

  ngOnInit() {
    this.items = [{ label: 'Home'}, { label: 'Profile' }];
    this.getProfile();
  }

  async getProfile() {
    var loggedUser = localStorage.getItem('loggedUser');
    if (loggedUser) {
      var userid = JSON.parse(loggedUser).id;
      this.userdata = await this.userService.getUserById(userid);
    } else console.log('Error: Invalid login detected. Relogin required');
  }
}
