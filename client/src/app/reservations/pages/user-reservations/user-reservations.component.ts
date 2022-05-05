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
  items: MenuItem[] | any;

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

  ngOnInit() {
    this.items = [{ label: 'User Reservation' }];
    this.fillTable();
  }


  async fillTable (){
    var loggedUser = localStorage.getItem('loggedUser');    
    if (loggedUser) {
      var userid = JSON.parse(loggedUser).id;
      this.allReservations = await this.reservationsService.getReservationById(userid);
      console.log(this.allReservations);
    } else console.log('Error: Invalid login detected. Relogin required');
  }
}