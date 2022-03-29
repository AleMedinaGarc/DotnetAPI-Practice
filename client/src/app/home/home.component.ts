import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  providers: [],
  styleUrls: [ './home.component.scss' ],

})
export class HomeComponent implements OnInit {
  title = '';
  response: Object | undefined;
  displayedColumns: string[] = ['Id', 'Username', 'FabricationYear', 'PlateNumber', 'NextITV'];
  constructor(private http: HttpClient) {

  }

  ngOnInit() {
    let obs = this.http.get('http://localhost:5000/api/Cars/allCars');
    try{
      obs.subscribe((response) => {
        this.response = response;
        console.log(response );
      })

    } catch(e) {
      console.log(e);
    }
  }


}
