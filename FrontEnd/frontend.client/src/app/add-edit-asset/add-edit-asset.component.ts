import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators, AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-add-edi-tasset',
  standalone: true,
  templateUrl: './add-edit-asset.component.html',
  styleUrl: './add-edit-asset.component.css',
  imports: [ReactiveFormsModule]
})
export class AddEditAssetComponent {
  public data: any = inject(MAT_DIALOG_DATA);

  assetForm = new FormGroup({
    name: new FormControl('', [Validators.required, this.nameInUseValidator.bind(this)]),
    type: new FormControl(null),
    isPercent: new FormControl(false)
  });

 nameInUseValidator(ctl: AbstractControl): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const name = ctl.value.trim().toLowerCase();
      const found = (this.data.assetNames as string[]).find(assetName => assetName.toLowerCase() === name)
      return found ? { nameInUse: { value: ctl.value } } : null;
    };
  }
}
