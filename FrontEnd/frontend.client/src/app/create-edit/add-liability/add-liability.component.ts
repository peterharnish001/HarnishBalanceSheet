import { Component, inject } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LiabilityModel } from '../models/liability.model';
import { CurrencyFormatDirective } from '../../currency-format.directive';

@Component({
  selector: 'app-add-liability.component',
  standalone: true,
  templateUrl: './add-liability.component.html',
  styleUrl: './add-liability.component.css',
  imports: [FormsModule, ReactiveFormsModule, CurrencyFormatDirective]
})
export class AddLiabilityComponent {
  public data: any = inject(MAT_DIALOG_DATA);
  public liability: LiabilityModel = new LiabilityModel();
  public nameValidationError: string = '';
  public valueValidationError: string = '';

  constructor(private dialogRef: MatDialogRef<AddLiabilityComponent>) {
  }

  onClickOk(): void {
    if (this.isValid()) {
      this.liability.name = this.liability.name.trim();
      this.dialogRef.close(this.liability);
    }
  }

  onClickCancel(): void {
    this.dialogRef.close(null);
  }

  isValid(): boolean {
    const res1 = this.validateName();
    const res2 = this.validateValue();
    return res1 && res2;
  }

  validateName(): boolean {
    if (this.liability.name.trim() === '') {
      this.nameValidationError = 'Name is required.';
      return false;
    } else if (this.data.liabilityNames.find((name : string) => name.toLowerCase() === this.liability.name.trim().toLowerCase())) {
       this.nameValidationError = 'Name must be unique.';
       return false;
    }

    this.nameValidationError = '';
    return true;
  }

  validateValue(): boolean {
      if (isNaN(this.liability.value)) {
        this.valueValidationError = 'Value must be numeric.';
        return false;
      } else if (this.liability.value === 0) {
        this.valueValidationError = 'Value must be greater than 0.';
        return false;
      }

    this.valueValidationError = '';
    return true;
  }
}
