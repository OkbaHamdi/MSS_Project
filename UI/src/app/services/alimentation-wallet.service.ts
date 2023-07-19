import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AlimentationWalletService {
  private baseUrl: string = 'https://localhost:7058/api/AlimentationWallet/';
  constructor(private http: HttpClient) {}
  AlimentationWallet(file: File, bankcode: string,affiliation:string) {
    const formData = new FormData();
    formData.append('file', file, file.name);

    return this.http.post<any>(`${this.baseUrl}AlimentationWallet?bankcode=${bankcode}&&affiliation=${affiliation}`, formData);
  }
}
