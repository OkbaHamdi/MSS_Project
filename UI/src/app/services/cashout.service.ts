import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CashoutService {
  private baseUrl: string = 'https://localhost:7058/api/AddWithdrawalTerminal/';
  constructor(private http: HttpClient) {}
  Cashout(file: File, bankcode: string) {
    const formData = new FormData();
    formData.append('file', file, file.name);

    return this.http.post<any>(`${this.baseUrl}AddWithdrawalTerminal?bankcode=${bankcode}`, formData);
  }
}
