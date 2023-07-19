import { Component, OnInit } from '@angular/core';
import { CashoutService } from 'src/app/services/cashout.service';

@Component({
  selector: 'app-cashout',
  templateUrl: './cashout.component.html',
  styleUrls: ['./cashout.component.scss']
})
export class CashoutComponent implements OnInit {
  CashoutData = {
    bankCode: "",
    file: null as File | null,
  };
  successMessage: string | null = null;
  errorMessage: string | null = null;
  constructor(private cashout:CashoutService) { }

  ngOnInit(): void {
  }
  onFileChange(event: any) {
    const fileList: FileList = event.target.files;
    if (fileList && fileList.length > 0) {
      this.CashoutData.file = fileList[0];
    }
  }
  CashOut() {
    const { file, bankCode } = this.CashoutData;
    if (file) {
      this.cashout.Cashout(file, bankCode).subscribe({
        next: (res) => {
          alert(res.message);
          this.successMessage = 'Cashout added successfully.';
          this.errorMessage = null;
        },
        error: (error) => {
          this.successMessage = null;
          this.errorMessage = error?.error?.Message || 'An error occurred.';
        },
      });
    }
  }

}
