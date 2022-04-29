import { Component, OnInit } from '@angular/core';
import { ColDef } from 'ag-grid-community';
import 'ag-grid-community/dist/styles/ag-grid.css';
import 'ag-grid-community/dist/styles/ag-theme-alpine.css';
import 'ag-grid-enterprise';
import { MenuItem } from 'primeng/api';
import { CarsService } from '../../cars.service'
import { Router } from '@angular/router';

@Component({
  selector: 'all-cars',                   
  templateUrl: './all-cars.component.html',  // Manages Component's HTML
  styleUrls: ['./all-cars.component.scss']    // Handles Component's styling
})

export class AllCarsComponent implements OnInit {
  allCars: any;
  carList: any;
  items: MenuItem[] | any;
  
  columnDefs: ColDef[] = [
    { field: 'bodyStyleCode'},
    { field: 'brand'},
    { field: 'cO2Emissions'},
    { field: 'cc'},
    { field: 'chassisBrand'},
    { field: 'chassisManufacturer'},
    { field: 'chassisVehicleBase'},
    { field: 'chassisVehicleType'},
    { field: 'chassisVehicleVariant'},
    { field: 'curbWeight'},
    { field: 'ecoCode'},
    { field: 'ecoInnovation'},
    { field: 'ecoReduction'},
    { field: 'electricPowerConsumption'},
    { field: 'electricVehicleAutonomy'},
    { field: 'electricVehicleCategory'},
    { field: 'engineIntakeType'},
    { field: 'enrollmentDate'},
    { field: 'euroEmissionLevel'},
    { field: 'fabricationYear'},
    { field: 'firstRegistrationDate'},
    { field: 'frontAxleTrack'},
    { field: 'fuelType'},
    { field: 'geoLocationCode'},
    { field: 'geoRegistrationCode'},
    { field: 'grossWeight'},
    { field: 'horsePower'},
    { field: 'ineCode'},
    { field: 'isGPSGPRSUnregistered'},
    { field: 'isNew'},
    { field: 'isRental'},
    { field: 'isSealed'},
    { field: 'isSeized'},
    { field: 'isStolen'},
    { field: 'isTemporarilyDeregistered'},
    { field: 'itvCode'},
    { field: 'itvType'},
    { field: 'location'},
    { field: 'manufacturer'},
    { field: 'maxCurbWeight'},
    { field: 'maxSeats'},
    { field: 'model'},
    { field: 'municipality'},
    { field: 'nextCarInspection'},
    { field: 'nextITV'},
    { field: 'numberPlate'},
    { field: 'originClass'},
    { field: 'ownerHasTitle'},
    { field: 'ownerType'},
    { field: 'owners'},
    { field: 'ownershipTypeCode'},
    { field: 'passengersPerSquareMeter'},
    { field: 'permanentlyUnregistered'},
    { field: 'postalCode'},
    { field: 'processingDate'},
    { field: 'productionModelCode'},
    { field: 'productionModelPassword'},
    { field: 'rearAxleTrack'},
    { field: 'registrationClass'},
    { field: 'registrationDate'},
    { field: 'registrationType'},
    { field: 'registrationTypeDate'},
    { field: 'seats'},
    { field: 'serviceCode'},
    { field: 'taxableHorsePower'},
    { field: 'transmissions'},
    { field: 'vehicleRegulationClass'},
    { field: 'vehicleType'},
    { field: 'vehicleVariant'},
    { field: 'vehicleVersion'},
    { field: 'vin'},
    { field: 'weightIndicator'},
    { field: 'wheelbase'},
  ];
    
  constructor(
    private carsService: CarsService,
    private router: Router
  ){}

  ngOnInit() {
    this.items = [{ label: 'Home', url: '' }, { label: 'All Cars' }];
    this.FillTable();
  }

  cellDoubleClicked($event: any){
    console.log($event.data.vin)
    const navigationDetails: string[] = ['/detailedCarData/' + $event.data.vin];
    this.router.navigate(navigationDetails);
  }

  async FillTable (){
    this.allCars = await this.carsService.getAllCars();
    var arrayList:any[] = Object.values(this.allCars)
    var companyCarList:any[] = arrayList[0];
    var dgtInfo:any[] = arrayList[1];
    this.carList = this.mergeByProperty(companyCarList, dgtInfo, 'vin');
    console.log(this.carList)
  }

  addCar(): void {
    const navigationDetails: string[] = ['/addCar'];
    this.router.navigate(navigationDetails);
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
}