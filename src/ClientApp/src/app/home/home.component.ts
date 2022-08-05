import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { positionElements } from '@ng-bootstrap/ng-bootstrap/util/positioning';
import { environment } from '../../environments/environment';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  private ApiBaseUrl: string = environment.API_URL_BASE;
  constructor(private http: HttpClient, 
    @Inject('BASE_URL') baseUrl: string) {
  }

  public async requestConnection() {
    let data = {
      PageOn : window.location.pathname
    }

    let result = await this.http.get<any>(this.ApiBaseUrl + "api/CaptivePortal/RequestGuestAccess").toPromise();
    if(result === true) {
      alert("Approved")
    }
    else {
      alert("Access Denied")
    }
  }
}
