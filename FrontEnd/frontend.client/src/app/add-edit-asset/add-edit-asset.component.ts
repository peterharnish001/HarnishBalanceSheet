import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators, AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CurrencyFormatDirective } from '../currency-format.directive';

@Component({
  selector: 'app-add-edi-tasset',
  standalone: true,
  templateUrl: './add-edit-asset.component.html',
  styleUrl: './add-edit-asset.component.css',
  imports: [ReactiveFormsModule, CurrencyFormatDirective]
})
export class AddEditAssetComponent {
  public data: any = inject(MAT_DIALOG_DATA);

  assetForm = new FormGroup({
    name: new FormControl('', [Validators.required]),
    type: new FormControl(null),
    isPercent: new FormControl(false),
    value: new FormControl({ value: 0, disabled: true})
  });

  isValueDisabled(): boolean {
    return this.assetForm.controls['type'].value === null
      && this.assetForm.controls['isPercent'].value === false;
  }

  onTypeOrIsPercentChange(): void {
    if (this.assetForm.controls['type'].value === null
      && this.assetForm.controls['isPercent'].value === false) {
        this.assetForm.controls['value'].disable();
      } else {
        this.assetForm.controls['value'].enable();
      }
  }

 nameInUseValidator(ctl: AbstractControl): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const name = ctl.value.trim().toLowerCase();
      const found = (this.data.assetNames as string[]).find(assetName => assetName.toLowerCase() === name)
      return found ? { nameInUse: { value: ctl.value } } : null;
    };
  }
}
