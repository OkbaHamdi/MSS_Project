import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CashinService {
  private baseUrl: string = 'https://localhost:7058/api/WTerminalsRefillWallet/';
  constructor(private http: HttpClient) {}
  Cashin(file: File, bankcode: string) {
    const formData = new FormData();
    formData.append('file', file, file.name);

    return this.http.post<any>(`${this.baseUrl}WTerminalsRefillWallet?bankcode=${bankcode}`, formData);
  }
}
