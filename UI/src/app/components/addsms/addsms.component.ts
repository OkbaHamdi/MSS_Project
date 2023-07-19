import { Component, OnInit } from '@angular/core';
import { AddsmsService } from 'src/app/services/addsms.service';
@Component({
  selector: 'app-addsms',
  templateUrl: './addsms.component.html',
  styleUrls: ['./addsms.component.scss']
})
export class AddsmsComponent implements OnInit {
  smsData = {
    smSid: "",
    smSpwd: '',
    bankCode:''
  };
  successMessage: string | null = null;
  errorMessage: string | null = null;
  constructor(private addsmsService: AddsmsService) { }

  ngOnInit(): void {
  }
  addSMS() {
      this.addsmsService.addSMS(this.smsData).subscribe({
        next: (res) => {
          alert(res.message);
          this.successMessage = 'SMS added successfully.';
          this.errorMessage = null;
        },
        error: (error) => {
          this.successMessage = null;
          this.errorMessage = error?.error?.Message || 'An error occurred.';
        },
      });
    
  }
}
