import { Component, OnInit } from '@angular/core';
import { ColDef } from 'ag-grid-community';
import 'ag-grid-community/dist/styles/ag-grid.css';
import 'ag-grid-community/dist/styles/ag-theme-alpine.css';
import 'ag-grid-enterprise';
import { MenuItem } from 'primeng/api';
import { ReservationsService } from '../../reservations.service';

@Component({
  selector: 'all-reservations',
  templateUrl: './all-reservations.component.html', // Manages Component's HTML
  styleUrls: ['./all-reservations.component.scss'], // Handles Component's styling
})
export class AllReservationsComponent implements OnInit {
  allReservations: any;
  items: MenuItem[] | any;

  columnDefs: ColDef[] = [
    { field: 'reservationId' },
    { field: 'userId' },
    { field: 'vin' },
    { field: 'fromDate' },
    { field: 'toDate' },
    { field: 'carUse' },
  ];

  constructor(private reservationsService: ReservationsService) {}

  ngOnInit() {
    this.items = [{ label: 'Home', url: '' }, { label: 'All Reservations' }];
    this.FillTable();
  }

  async FillTable() {
    this.allReservations = await this.reservationsService.getAllReservations();
    console.log(this.allReservations);
  }
}
