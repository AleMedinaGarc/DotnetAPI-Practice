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

  constructor(private http: HttpClient, private router: Router) {}

  public async getAllReservations(): Promise<Object> {
    await this.getReservations();
    return this.getResponse!;
  }

  public async getReservationById(id: string | undefined): Promise<Object> {
    await this.getReservation(id);
    return this.getResponse!;
  }

  public async addReservation(reservation: {
    userId: string;
    vin: string;
    fromDate: string;
    toDate: string;
    carUse: string;
  }) {
    return await this.postReservation(reservation);
  }

  private async getReservations() {
    const obs = this.http.get(
      'http://localhost:5000/api/Reservations/allReservations',
      this.getHeaderAuth()
    );

    return this.generatePetition(obs);
  }

  private async getReservation(id: string | undefined) {
    const obs = this.http.get(
      'http://localhost:5000/api/Reservations/userReservations/' + id,
      this.getHeaderAuth()
    );

    return this.generatePetition(obs);
  }

  private async postReservation(reservation: {
    userId: string;
    vin: string;
    fromDate: string;
    toDate: string;
    carUse: string;
  }) {
    const body = {
      userId: reservation.userId,
      vin: reservation.vin,
      fromDate: reservation.fromDate,
      toDate: reservation.toDate,
      carUse: reservation.carUse,
    };
    const obs = this.http.post<string>(
      'http://localhost:5000/api/Reservations/addReservation',
      body,
      this.getHeaderAuth('text')
    );

    const promise = new Promise<void>((resolve, reject) => {
      obs.subscribe({
        next: (res: any) => {
          this.getResponse = res;
          this.router.navigate(['/userReservations']);
          resolve();
        },
        error: (err: any) => {
          reject(err);
        },
      });
    });

    return promise;
  }

  private generatePetition(obs: any) {
    const promise = new Promise<void>((resolve, reject) => {
      obs.subscribe({
        next: (res: any) => {
          this.getResponse = res;
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
      responseType: resType ? (resType as 'json') : 'json',
    };
    return httpOptions;
  }
}
