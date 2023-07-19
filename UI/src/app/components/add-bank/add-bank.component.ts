import { Component, OnInit } from '@angular/core';
import { AddBankService } from 'src/app/services/add-bank.service';
@Component({
  selector: 'app-add-bank',
  templateUrl: './add-bank.component.html',
  styleUrls: ['./add-bank.component.scss']
})
export class AddBankComponent implements OnInit {
  BankData = {
    bankCode: "",
    codeApp: ''
  };
  successMessage: string | null = null;
  errorMessage: string | null = null;
  constructor(private addbank : AddBankService) { }

  ngOnInit(): void {
  }
  addBank() {
    console.log(this.BankData)
      this.addbank.add(this.BankData).subscribe({
        next: (res) => {
          alert(res.message);
          this.successMessage = 'Bank added successfully.';
          this.errorMessage = null;
        },
        error: (error) => {
          this.successMessage = null;
          this.errorMessage = error?.error?.Message || 'An error occurred.';
        },
      });
    
  }
}
