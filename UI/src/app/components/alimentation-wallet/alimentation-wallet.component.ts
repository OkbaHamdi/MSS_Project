import { Component, OnInit } from '@angular/core';
import { AlimentationWalletService } from 'src/app/services/alimentation-wallet.service';
@Component({
  selector: 'app-alimentation-wallet',
  templateUrl: './alimentation-wallet.component.html',
  styleUrls: ['./alimentation-wallet.component.scss']
})
export class AlimentationWalletComponent implements OnInit {

  AlimentationWalletData = {
    bankCode: "",
    affiliation:"",
    file: null as File | null,
  };
  successMessage: string | null = null;
  errorMessage: string | null = null;
  constructor(private alimentationwallet:AlimentationWalletService) { }

  ngOnInit(): void {
  }
  onFileChange(event: any) {
    const fileList: FileList = event.target.files;
    if (fileList && fileList.length > 0) {
      this.AlimentationWalletData.file = fileList[0];
    }
  }
  AlimentationWallet() {
    const { file, bankCode,affiliation } = this.AlimentationWalletData;
    if (file) {
      this.alimentationwallet.AlimentationWallet(file, bankCode,affiliation).subscribe({
        next: (res) => {
          alert(res.message);
          this.successMessage = 'Successfully added.';
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
