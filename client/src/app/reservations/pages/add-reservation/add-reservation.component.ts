import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'add-reservation',
  templateUrl: './add-reservation.component.html', // Manages Component's HTML
  styleUrls: ['./add-reservation.component.scss'], // Handles Component's styling
})


export class AddReservationComponent {
  disabled: boolean = true;
  carUse: any[];
  reservationObject: Object;
  items: MenuItem[] | any;

 

  profileForm = new FormGroup({
    vin: new FormControl(this.route.snapshot.params['id'], Validators.required),
    fromDate: new FormControl('', Validators.required),
    toDate: new FormControl('', Validators.required),
    carUser: new FormControl('', Validators.required),
  });

  constructor(private route: ActivatedRoute) {
    this.carUse = [
      { label: 'Personal', value: 'personal' },
      { label: 'Shared', value: 'shared' },
    ];
    this.reservationObject = {
      vin: '',
      fromDate: '',
      toDate: '',
      carUser: ''
    }
  }

  ngOnInit() {
    this.items = [{ label: 'Home', url: '' }, { label: 'Add Reservation' }];
  }

  onSubmit() {
    for (const field in this.profileForm.controls) {

      this.reservationObject = this.profileForm.controls[field].value as string ;
      console.log(this.reservationObject);
    }
  }
}
