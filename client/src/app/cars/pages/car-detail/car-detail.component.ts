import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { CarsService } from '../../cars.service'

@Component({
  selector: 'car-detail-component',                   
  templateUrl: './car-detail.component.html',  // Manages Component's HTML
  styleUrls: ['./car-detail.component.scss']    // Handles Component's styling
})

export class CarDetailComponent implements OnInit {
  cardata: any;
  items: MenuItem[] | any;
    
  constructor(private carsService: CarsService, private route: ActivatedRoute) { 
   this.cardata = {
    'bodyStyleCode': '',
    'brand': '',
    'cO2Emissions': '',
    'cc': '',
    'chassisBrand': '',
    'chassisManufacturer': '',
    'chassisVehicleBase': '',
    'chassisVehicleType': '',
    'chassisVehicleVariant': '',
    'curbWeight': '',
    'ecoCode': '',
    'ecoInnovation': '',
    'ecoReduction': '',
    'electricPowerConsumption': '',
    'electricVehicleAutonomy': '',
    'electricVehicleCategory': '',
    'engineIntakeType': '',
    'enrollmentDate': '',
    'euroEmissionLevel': '',
    'fabricationYear': '',
    'firstRegistrationDate': '',
    'frontAxleTrack': '',
    'fuelType': '',
    'geoLocationCode': '',
    'geoRegistrationCode': '',
    'grossWeight': '',
    'horsePower': '',
    'ineCode': '',
    'isGPSGPRSUnregistered': '',
    'isNew': '',
    'isRental': '',
    'isSealed': '',
    'isSeized': '',
    'isStolen': '',
    'isTemporarilyDeregistered': '',
    'itvCode': '',
    'itvType': '',
    'location': '',
    'manufacturer': '',
    'maxCurbWeight': '',
    'maxSeats': '',
    'model': '',
    'municipality': '',
    'nextCarInspection': '',
    'nextITV': '',
    'numberPlate': '',
    'originClass': '',
    'ownerHasTitle': '',
    'ownerType': '',
    'owners': '',
    'ownershipTypeCode': '',
    'passengersPerSquareMeter': '',
    'permanentlyUnregistered': '',
    'postalCode': '',
    'processingDate': '',
    'productionModelCode': '',
    'productionModelPassword': '',
    'rearAxleTrack': '',
    'registrationClass': '',
    'registrationDate': '',
    'registrationType': '',
    'registrationTypeDate': '',
    'seats': '',
    'serviceCode': '',
    'taxableHorsePower': '',
    'transmissions': '',
    'vehicleRegulationClass': '',
    'vehicleType': '',
    'vehicleVariant': '',
    'vehicleVersion': '',
    'vin': '',
    'weightIndicator': '',
    'wheelbase': '',
   }
  }   // Used to inject dependencies

  ngOnInit() {
    this.items = [{ label: 'Home', url: '' }, { label: 'All Cars' }, { label: 'Car Detail' }];
    this.getCarData();
  }

  async getCarData(){
    var arrayList = Object.values(await this.carsService.getAllCars());
    var companyCarList:any[] = arrayList[0];
    var dgtInfo:any[] = arrayList[1];
    this.cardata = companyCarList.find(item => item.vin == this.route.snapshot.params['id']);
    this.cardata = dgtInfo.find(item => item.vin == this.route.snapshot.params['id']);
  }
}