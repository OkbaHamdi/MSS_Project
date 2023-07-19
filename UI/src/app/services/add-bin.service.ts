import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AddBinService {
private baseUrl: string = 'https://localhost:7058/api/AddBin/';
constructor(private http: HttpClient) {}
addBin(Obj: any) {
  return this.http.post<any>(`${this.baseUrl}AddBin`, Obj);
  }
}
