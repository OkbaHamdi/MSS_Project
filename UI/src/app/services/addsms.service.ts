import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class AddsmsService {
  private baseUrl: string = 'https://localhost:7058/api/AddSMS/';
  constructor(private http: HttpClient) { }
  addSMS(Obj: any) {
    return this.http.post<any>(`${this.baseUrl}AddSMS`, Obj);
  }
}

