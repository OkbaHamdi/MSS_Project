import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AddBinFileService {
  private baseUrl: string = 'https://localhost:7058/api/AddBinFile/';

  constructor(private http: HttpClient) {}

  AddBinFile(file: File, bankcode: string, stock: boolean) {
    const formData = new FormData();
    formData.append('file', file, file.name);

    return this.http.post<any>(`${this.baseUrl}AddBinFile?bankcode=${bankcode}&stock=${stock}`, formData);
  }
}
