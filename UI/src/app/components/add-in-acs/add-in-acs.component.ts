import { Component, OnInit } from '@angular/core';
import { AddInACSService } from 'src/app/services/add-in-acs.service';
@Component({
  selector: 'app-add-in-acs',
  templateUrl: './add-in-acs.component.html',
  styleUrls: ['./add-in-acs.component.scss']
})
export class AddInACSComponent implements OnInit {
  ACSData = {
    bankCode: ""
  };
  successMessage: string | null = null;
  errorMessage: string | null = null;
  constructor(private  addinACSService:AddInACSService) { }

  ngOnInit(): void {
  }
  updateACS() {
    this.addinACSService.addinACS(this.ACSData).subscribe({
      next: (res) => {
        alert(res.message);
        this.successMessage = 'ACS Updated successfully.';
        this.errorMessage = null;
      },
      error: (error) => {
        this.successMessage = null;
        this.errorMessage = error?.error?.Message || 'An error occurred.';
      },
    });
  }
}
