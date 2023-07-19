import { Component, OnInit } from '@angular/core';
import { AddTokenService } from 'src/app/services/add-token.service';
@Component({
  selector: 'app-add-token',
  templateUrl: './add-token.component.html',
  styleUrls: ['./add-token.component.scss']
})
export class AddTokenComponent implements OnInit {
  TokenData = {
    bankCode: "",
    token: '',
  };
  successMessage: string | null = null;
  errorMessage: string | null = null;
  constructor(private addTokenService:AddTokenService) { }

  ngOnInit(): void {
  }
  addToken() {
    this.addTokenService.addToken(this.TokenData).subscribe({
      next: (res) => {
        alert(res.message);
        this.successMessage = 'Token added successfully.';
        this.errorMessage = null;
      },
      error: (error) => {
        this.successMessage = null;
        this.errorMessage = error?.error?.Message || 'An error occurred.';
      },
    });
  }

}
