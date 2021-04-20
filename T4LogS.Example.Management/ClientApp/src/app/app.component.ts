import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  public data: IT4LogSReadObject[];
  http: HttpClient;
  baseUrl: string;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.getData(null, true).subscribe(result => {
      this.data = result;
    }, error => console.error(error));
  }

  getData(obj: IT4LogSReadObject, lazyLoading: boolean) {
    return this.http.post<IT4LogSReadObject[]>(this.baseUrl + 'api/GetData/GetPath?lazyLoading=' + lazyLoading, obj,
      {
        headers: new HttpHeaders({
          'Content-Type': 'application/json; charset=utf-8',
          'Accept': 'application/json',
        })
      }
    )
  }

  numberToArray(num: number) {
    return new Array(num);
  }
  
  encodeFormatRoute(obj) {
    return encodeURIComponent(JSON.stringify(obj));
  }

  decodeFormatRoute(obj: string) {
    return JSON.parse(decodeURIComponent(obj));
  }
}

interface IT4LogSReadObject {
  Parent: string;
  Content: string;
  Name: string;
  Location: string;
  IsFile: boolean;
  Level: number;
}