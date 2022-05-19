import { Component, HostListener, OnInit } from '@angular/core';
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
  innerWidth: any;
  items: MenuItem[] | any;

  constructor(
    public socialAuthService: SocialAuthService,
    private carServices: CarsService,
    private router: Router
  ) {}

  ngOnInit() {
    this.items = [{ label: 'Home'}, { label: 'All Reservations' }];
    this.GetCarList();
    this.innerWidth = window.innerWidth;
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

  @HostListener('window:resize', ['$event'])
  onResize() {
    this.innerWidth = window.innerWidth;
    console.log(this.innerWidth)
  }
  // 1150
}
