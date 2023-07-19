import { Component, OnInit } from '@angular/core';
import { AddBinService } from 'src/app/services/add-bin.service';
@Component({
  selector: 'app-add-bin',
  templateUrl: './add-bin.component.html',
  styleUrls: ['./add-bin.component.scss']
})
export class AddBinComponent implements OnInit {
  BinData = {
    bankCode: "",
    binID: '',
    stock:true
  };
  successMessage: string | null = null;
  errorMessage: string | null = null;
  constructor(private addBinService:AddBinService) { }

  ngOnInit(): void {
  }
  addBin() {
    this.addBinService.addBin(this.BinData).subscribe({
      next: (res) => {
        alert(res.message);
        this.successMessage = 'BIN added successfully.';
        this.errorMessage = null;
      },
      error: (error) => {
        this.successMessage = null;
        this.errorMessage = error?.error?.Message || 'An error occurred.';
      },
    });
  }
}
