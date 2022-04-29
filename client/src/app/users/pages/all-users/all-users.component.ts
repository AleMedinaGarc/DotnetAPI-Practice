import { Component, OnInit } from '@angular/core';
import { ColDef } from 'ag-grid-community';
import 'ag-grid-community/dist/styles/ag-grid.css';
import 'ag-grid-community/dist/styles/ag-theme-alpine.css';
import 'ag-grid-enterprise';
import { MenuItem } from 'primeng/api';
import { UserService } from '../../users.service';
@Component({
  selector: 'all-users-component',                   
  templateUrl: './all-users.component.html',  // Manages Component's HTML
  styleUrls: ['./all-users.component.scss']    // Handles Component's styling
})

export class AllUsersComponent implements OnInit{
  allUsers: any;

  columnDefs: ColDef[] = [
    { field: 'userId' },
    { field: 'givenName' },
    { field: 'familyName'},
    { field: 'fullName'},
    { field: 'email'},
    { field: 'role'},
    { field: 'phoneNumber'},
    { field: 'addressFormatted'},
    { field: 'department'},
    { field: 'creationDate'},
    { field: 'lastLogin'},
  ];

  constructor(
    private userService: UserService
  ){}

  items: MenuItem[] | any;
  ngOnInit() {
    this.items = [{ label: 'Home', url: '' }, { label: 'All Users' }];
    this.FillTable();
  }


  async FillTable (){
    this.allUsers = await this.userService.getAllUsers();
    console.log(this.allUsers)
  }
}
