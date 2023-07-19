import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AddTokenService {
  private baseUrl: string = 'https://localhost:7058/api/AddToken/';
  constructor(private http: HttpClient) {}
  addToken(Obj:any){
    return this.http.post<any>(`${this.baseUrl}AddToken`, Obj);
  }
}
