import { Component, OnInit } from '@angular/core';
import { ColDef } from 'ag-grid-community';
import 'ag-grid-community/dist/styles/ag-grid.css';
import 'ag-grid-community/dist/styles/ag-theme-alpine.css';
import 'ag-grid-enterprise';
import { MenuItem } from 'primeng/api';
import { ReservationsService } from '../../reservations.service'

@Component({
  selector: 'user-reservations',                   
  templateUrl: './user-reservations.component.html',  // Manages Component's HTML
  styleUrls: ['./user-reservations.component.scss']    // Handles Component's styling
})

export class UserReservationsComponent implements OnInit {
  allReservations: any;

  columnDefs: ColDef[] = [
    { field: 'reservationId' },
    { field: 'userId' },
    { field: 'vin'},
    { field: 'fromDate'},
    { field: 'toDate'},
    { field: 'carUse'}
  ];

  constructor(
    private reservationsService: ReservationsService
  ){}

  items: MenuItem[] | any;
  ngOnInit() {
    this.items = [{ label: 'Home', url: '' }, { label: 'User Reservation' }];
    this.FillTable();
  }


  async FillTable (){
    var loggedUser = localStorage.getItem('loggedUser');    
    if (loggedUser) {
      var userid = JSON.parse(loggedUser).id;
      console.log(userid);
      this.allReservations = await this.reservationsService.getReservationById(userid);
    } else console.log('Error: Invalid login detected. Relogin required');
  }
}