import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MenuItem } from 'primeng/api';
import { CarsService } from '../../cars.service';

@Component({
  selector: 'add-car',
  templateUrl: './add-car.component.html', // Manages Component's HTML
  styleUrls: ['./add-car.component.scss'], // Handles Component's styling
})
export class AddCarComponent {
  items: MenuItem[] | any;
  carObject: any;
  submited: boolean = false;
  error: string  | any;
  
  profileForm = new FormGroup({
    vin: new FormControl('RP8MD4100MV109768', Validators.required),
    numberPlate: new FormControl('4658EGT', Validators.required),
    fabricationYear: new FormControl(2024, Validators.required),
    nextITV: new FormControl('09-2024', Validators.required),
    nextCarInspection: new FormControl('09-2022', Validators.required),
  });

  ngOnInit() {
    this.items = [
      { label: 'All Cars' },
      { label: 'Add Car' },
    ];

  }

  constructor(
    private carsService: CarsService
  ) {
    this.carObject = {
      vin: '',
      numberPlate: '',
      fabricationYear: 0,
      nextITV: '',
      nextCarInspection: '',
    };
  }

  crumClicked(event: { item: any; }) {
    console.log(event.item);
  }
  onSubmit() {
    for (const field in this.profileForm.controls) {
      if(field == 'fabricationYear')
      this.carObject[field] = this.profileForm.controls[field].value as number;
      else this.carObject[field] = this.profileForm.controls[field].value;
    }
    console.log(this.carObject)
    this.createPost();
  }

  async createPost() {
    await this.carsService.addCar(this.carObject).then((result)=>{
      console.log(result);     
    }).catch((err: any)=>{
      switch (err.status){
        case 409: this.error="Car already taken. Please choose another available car."; break;
        default: this.error="Unexpected error."; break;
      }
    }); 
  }
}
