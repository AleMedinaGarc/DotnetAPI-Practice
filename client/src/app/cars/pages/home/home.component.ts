import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SocialAuthService } from '@abacritt/angularx-social-login';
import { CarsService } from '../../cars.service'
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'home-component',
  templateUrl: './home.component.html',
  providers: [],
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  response: Object | undefined;
  carList : any;

  constructor(
    public socialAuthService: SocialAuthService,
    private carServices: CarsService,
    private router: Router
  ) {}

  items: MenuItem[] | any;
  ngOnInit() {
    this.items = [{ label: 'Home', url: '' }, { label: 'All Reservations' }];
    this.GetCarList();
  }

  reserveClick($myParam: string = ''): void {
    const navigationDetails: string[] = ['/addReservation'];
    if($myParam.length) {
      navigationDetails.push($myParam);
    }
    this.router.navigate(navigationDetails);
  }

  async GetCarList(){
    this.carServices.getAllAvailableCars().then((response) => {
      var arrayList:any[] = Object.values(response)
      var companyCarList:any[] = arrayList[0];
      var dgtInfo:any[] = arrayList[1];
      this.carList = this.mergeByProperty(companyCarList, dgtInfo, 'vin');
      console.log(this.carList)
     })
  }

  private mergeByProperty (target: any[], source: any[], prop: string | number) {
    source.forEach((sourceElement: { [x: string]: any; }) => {
      let targetElement = target.find((targetElement: { [x: string]: any; }) => {
        return sourceElement[prop] === targetElement[prop];
      })
      targetElement ? Object.assign(targetElement, sourceElement) : target.push(sourceElement);
    })
    return target;
  }
  // allCars = service.getallcars();
  // allReservations = service.getallreserved()
  // for all items in allcars
  // for all items in reservations
  // if reservations.vin = allcars.vin
  //remove allCars[0][item.vin]
  //remove allCars[1][item.vin]
  // for item in allCars[0]
  // create card with (new component)
  // brand
  // model
  // fuel type
  // seats

  printContent(test: any) {
    console.log(test);
  }
}
