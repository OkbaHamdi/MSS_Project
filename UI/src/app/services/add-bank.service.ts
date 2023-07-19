import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AddBankService {
  private baseUrl: string = 'https://localhost:7058/api/MobileService/AddBank';
  constructor(private http: HttpClient) { }
  add(Obj: any) {
    return this.http.post<any>(`${this.baseUrl}`, Obj);
  }
}
