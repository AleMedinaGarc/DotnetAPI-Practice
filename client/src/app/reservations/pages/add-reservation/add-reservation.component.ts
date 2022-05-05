import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { ReservationsService } from '../../reservations.service';

@Component({
  selector: 'add-reservation',
  templateUrl: './add-reservation.component.html', // Manages Component's HTML
  styleUrls: ['./add-reservation.component.scss'], // Handles Component's styling
})
export class AddReservationComponent {
  disabled: boolean = true;
  carUse: any[];
  reservationObject: any;
  items: MenuItem[] | any;

  profileForm = new FormGroup({
    vin: new FormControl(this.route.snapshot.params['id'], Validators.required),
    fromDate: new FormControl('', Validators.required),
    toDate: new FormControl('', Validators.required),
    carUse: new FormControl('', Validators.required),
  });

  constructor(
    private route: ActivatedRoute,
    private reservationsService: ReservationsService,
  ) {
    this.carUse = [
      { label: 'Personal', value: 'personal' },
      { label: 'Shared', value: 'shared' },
    ];
    this.reservationObject = {
      userId: '',
      vin: '',
      fromDate: '',
      toDate: '',
      carUse: '',
    };
  }

  ngOnInit() {
    this.items = [{ label: 'Home' }, { label: 'Add Reservation' }];
  }

  onSubmit() {
    const user = localStorage.getItem('loggedUser') || '{}';
    this.reservationObject['userId'] = JSON.parse(user).id;
    for (const field in this.profileForm.controls) {
      if (field == 'toDate' || field == 'fromDate') {
        const date = this.profileForm.controls[field].value;
        this.reservationObject[field] = this.formatDate(date);
      } else
        this.reservationObject[field] = this.profileForm.controls[field].value;
    }
    console.log(this.reservationObject)
    this.createPost();
  }

  async createPost() {
    await this.reservationsService.addReservation(this.reservationObject);
  }

  formatDate(date: any): string {
    var newDate = [];
    newDate.push(('00' + date.getDate()).slice(-2));
    newDate.push(('00' + (date.getMonth() + 1)).slice(-2)); // add 1 because months are indexed from 0
    newDate.push(date.getFullYear());
    return newDate[0] + '-' + newDate[1] + '-' + newDate[2];
  }
}
