import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
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
    name: new FormControl('', [Validators.required])
  });
}
