import { Component, OnInit } from '@angular/core';
import { AddBinFileService } from 'src/app/services/add-bin-file.service';

@Component({
  selector: 'app-add-bin-file',
  templateUrl: './add-bin-file.component.html',
  styleUrls: ['./add-bin-file.component.scss']
})
export class AddBinFileComponent implements OnInit {
  BinFileData = {
    bankCode: "",
    file: null as File | null,
    stock: true
  };
  successMessage: string | null = null;
  errorMessage: string | null = null;

  constructor(private addbinfileService: AddBinFileService) { }

  ngOnInit(): void {
  }

  onFileChange(event: any) {
    const fileList: FileList = event.target.files;
    if (fileList && fileList.length > 0) {
      this.BinFileData.file = fileList[0];
    }
  }

  AddBinFile() {
    const { file, bankCode, stock } = this.BinFileData;
    if (file) {
      this.addbinfileService.AddBinFile(file, bankCode, stock).subscribe({
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
}
