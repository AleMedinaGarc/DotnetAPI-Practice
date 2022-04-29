import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { SocialAuthService } from '@abacritt/angularx-social-login';

@Injectable({
  providedIn: 'root',
})
export class CarsService {
  allCars: Object | undefined; // [[dbCars],[dgtCars]]
  carsTaken: any; // [reservations.vin]
  getResponse: Object | undefined;

  constructor(private http: HttpClient) {}

  public async getAllAvailableCars(): Promise<Object>{
    await this.getCars();
    //this.getCarsTaken();
    return this.getResponse!;
  }

  public async getAllCars() {
    await this.getCars();
    return this.getResponse!;
  }
  private async getCars() {
    const jwt = localStorage.getItem('jwt');
    const headers_object = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + jwt,
    });

    const httpOptions = {
      headers: headers_object,
    };

    const obs = this.http.get(
      'http://localhost:5000/api/CompanyCars/allCars',
      httpOptions
    );
    return this.generatePetition(obs);
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
}