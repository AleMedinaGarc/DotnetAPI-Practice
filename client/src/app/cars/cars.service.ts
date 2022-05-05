import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { SocialAuthService } from '@abacritt/angularx-social-login';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class CarsService {
  allCars: Object | undefined; // [[dbCars],[dgtCars]]
  carsTaken: any; // [reservations.vin]
  getResponse: Object | undefined;

  constructor(private http: HttpClient, private router: Router) {}

  public async getAllAvailableCars(): Promise<Object>{
    await this.getCars();
    //this.getCarsTaken();
    return this.getResponse!;
  }

  public async getAllCars() {
    await this.getCars();
    return this.getResponse!;
  }

  public async addCar(car: {
    vin: string;
    numberPlate: string;
    fabricationYear: number;
    nextITV: string;
    nextCarInspection: string;
  }) {
    await this.postCar(car);
  }

  private async getCars() {
    const obs = this.http.get(
      'http://localhost:5000/api/CompanyCars/allCars',
      this.getHeaderAuth()
    );
    return this.generatePetition(obs);
  }

  private async postCar(car: {
    vin: string;
    numberPlate: string;
    fabricationYear: number;
    nextITV: string;
    nextCarInspection: string;
  }) {
    const body = {
      vin: car.vin,
      numberPlate: car.numberPlate,
      fabricationYear: car.fabricationYear,
      nextITV: car.nextITV,
      nextCarInspection: car.nextCarInspection,
    };
    console.log(body);
    const obs = this.http.post<string>(
      'http://localhost:5000/api/CompanyCars/addCar',
      body,
      this.getHeaderAuth('text')
    );
    try {
      obs.subscribe((response) => {
        console.log(response);
      });
    } catch (e) {
      console.log(e);
    }
  }

  private generatePetition(obs: any){
    const promise = new Promise<void>((resolve, reject) => {
      obs.subscribe({
        next: (res: any) => {
          this.getResponse = res;
          console.log(res)
          resolve();
        },
        error: (err: any) => {
          reject(err);
        },
        complete: () => {
          console.log('complete');
        },
      });
    });
    return promise;
  }

  private getHeaderAuth(resType?: string) {
    const jwt = localStorage.getItem('jwt');
    const headers_object = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + jwt,
    });
    const httpOptions = {
      headers: headers_object,
      responseType: resType ? resType as 'json' : 'json',
    };
    return httpOptions;
  }
}