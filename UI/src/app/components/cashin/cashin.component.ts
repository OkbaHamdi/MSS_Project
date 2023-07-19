import { Component, OnInit } from '@angular/core';
import { CashinService } from 'src/app/services/cashin.service';

@Component({
  selector: 'app-cashin',
  templateUrl: './cashin.component.html',
  styleUrls: ['./cashin.component.scss']
})
export class CashinComponent implements OnInit {
  CashinData = {
    bankCode: "",
    file: null as File | null,
  };
  successMessage: string | null = null;
  errorMessage: string | null = null;
  constructor(private cashin:CashinService) { }
  ngOnInit(): void {
  }
  onFileChange(event: any) {
    const fileList: FileList = event.target.files;
    if (fileList && fileList.length > 0) {
      this.CashinData.file = fileList[0];
    }
  }
  Cashin() {
    const { file, bankCode } = this.CashinData;
    if (file) {
      this.cashin.Cashin(file, bankCode).subscribe({
        next: (res) => {
          alert(res.message);
          this.successMessage = 'Cashin added successfully.';
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
