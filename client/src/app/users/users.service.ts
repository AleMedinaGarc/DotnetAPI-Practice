import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  postId: any;
  user: any;
  getResponse: any;

  constructor(private http: HttpClient, private router: Router) {}

  public async postUser(user: {
    id: string;
    name: string;
    firstName: string;
    lastName: string;
    photoUrl: string;
    email: string;
  }) {
    const body = {
      userId: user.id,
      fullName: user.name,
      givenName: user.firstName,
      familyName: user.lastName,
      imageURL: user.photoUrl,
      email: user.email,
    };
    console.log(body);
    const obs = this.http.post<any>('http://localhost:5000/api/login', body, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    });
    try {
      obs.subscribe((response) => {
        const token = (<any>response).token;
        localStorage.setItem('jwt', token);
        this.router.navigate(['']);
      });
    } catch (e) {
      console.log(e);
    }
  }

  public async getAllUsers(): Promise<Object> {
    await this.getUsers();
    return this.getResponse!;
  }

  public async getUserById(id: string | undefined): Promise<Object> {
    await this.getUser(id);
    return this.getResponse!;
  }

  public async postReservationByUser(reservation: {
    userId: string;
    vin: string;
    fromDate: string;
    toDate: string;
    carUse: string;
  }) {
    await this.postReservation(reservation);
  }

  private async getUsers() {
    const obs = this.http.get(
      'http://localhost:5000/api/UserData/allUsers',
      this.getHeaderAuth()
    );
    return this.generatePetition(obs);
  }

  private async getUser(id: string | undefined) {
    const obs = this.http.get(
      'http://localhost:5000/api/UserData/' + id,
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
      fullName: reservation.vin,
      givenName: reservation.fromDate,
      familyName: reservation.toDate,
      imageURL: reservation.carUse,
    };
    const obs = this.http.post<any>(
      'http://localhost:5000/api/Reservations/addReservation',
      body,
      this.getHeaderAuth()
    );
    try {
      obs.subscribe((response) => {
        console.log(response);
        this.router.navigate(['/myReservations']);
      });
    } catch (e) {
      console.log(e);
    }
  }
  private generatePetition(obs: any) {
    const promise = new Promise<void>((resolve, reject) => {
      obs.subscribe({
        next: (res: any) => {
          this.getResponse = res;
          console.log(res);
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

  private getHeaderAuth() {
    const jwt = localStorage.getItem('jwt');
    const headers_object = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + jwt,
    });

    const httpOptions = {
      headers: headers_object,
    };
    return httpOptions;
  }
}
