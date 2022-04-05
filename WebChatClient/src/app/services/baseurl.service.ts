import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BaseurlService {

  baseUrl = "https://localhost:44331";
  constructor() { }

  getBaseUrl(){
    return this.baseUrl;
  }
}
