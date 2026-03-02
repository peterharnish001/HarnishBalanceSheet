import { Component, inject } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CurrencyFormatDirective } from '../currency-format.directive';
import { PercentFormatDirective } from '../percentage-format.directive';
import { AddEditAssetModel } from '../create-edit/models/addeditasset.model';

@Component({
  selector: 'app-add-edi-tasset',
  standalone: true,
  templateUrl: './add-edit-asset.component.html',
  styleUrl: './add-edit-asset.component.css',
  imports: [FormsModule, ReactiveFormsModule, CurrencyFormatDirective, PercentFormatDirective]
})
export class AddEditAssetComponent {
  public data: any = inject(MAT_DIALOG_DATA);
  public asset: AddEditAssetModel = this.data.asset;

  public nameValidationError: string = '';
  public valueValidationError: string = '';

  constructor() {}

  isValueDisabled(): boolean {
    return this.asset.type === null && !this.asset.isPercent;
  }
}
