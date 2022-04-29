import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class ReservationsService {
  postId: any;
  user: any;
  getResponse: any;

  constructor(
    private http: HttpClient) {
    }
    
  public async getAllReservations(): Promise<Object>{
    await this.getReservations();
    return this.getResponse!;
  }

  public async getReservationById(id: string | undefined): Promise<Object>{
    await this.getReservation(id);
    return this.getResponse!;
  }

  private async getReservations(){
    const jwt = localStorage.getItem('jwt');
    const headers_object = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + jwt,
    });

    const httpOptions = {
      headers: headers_object,
    };

    const obs = this.http.get(
      'http://localhost:5000/api/Reservations/allReservations',
      httpOptions
    );

    return this.generatePetition(obs);
  }

  private async getReservation(id: string | undefined){
    const jwt = localStorage.getItem('jwt');
    const headers_object = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + jwt,
    });

    const httpOptions = {
      headers: headers_object,
    };

    const obs = this.http.get(
      'http://localhost:5000/api/Reservations/' + id,
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
