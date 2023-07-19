import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AddInACSService {
  private baseUrl: string = 'https://localhost:7058/api/AddInACS/';
  constructor(private http: HttpClient) {} 
  addinACS(Obj: any) {
    return this.http.post<any>(`${this.baseUrl}ConfigACS`, Obj);
  } 
}
