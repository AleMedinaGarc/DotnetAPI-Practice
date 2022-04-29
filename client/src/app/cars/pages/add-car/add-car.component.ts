import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'add-car',
  templateUrl: './add-car.component.html', // Manages Component's HTML
  styleUrls: ['./add-car.component.scss'], // Handles Component's styling
})
export class AddCarComponent {
  items: MenuItem[] | any;
  
  profileForm = new FormGroup({
    vin: new FormControl('', Validators.required),
    numberPlate: new FormControl('', Validators.required),
    fabricationYear: new FormControl('', Validators.required),
    nextITV: new FormControl('', Validators.required),
    nextCarInspection: new FormControl('', Validators.required),
  });

  ngOnInit() {
    this.items = [
      { label: 'Home', url: '' },
      { label: 'All Cars' },
      { label: 'Add Car' },
    ];
  }

  onSubmit() {
    this.profileForm.get('vin');
    console.log('reactive form submitted');
    console.log(this.profileForm);
  }
}
