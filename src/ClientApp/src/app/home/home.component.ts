import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {}

  requestConnection() {
    

  }
}
