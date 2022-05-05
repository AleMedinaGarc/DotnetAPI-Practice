import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-footer',                   
  templateUrl: './footer.component.html',  // Manages Component's HTML
  styleUrls: ['./footer.component.scss']    // Handles Component's styling
})

export class FooterComponent implements OnInit {
  constructor() { }   // Used to inject dependencies
  ngOnInit() {  // Lifecycle hook, initialize when component class is ready
  }
}