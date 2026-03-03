import { Component, inject, signal } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CurrencyFormatDirective } from '../currency-format.directive';
import { PercentFormatDirective } from '../percentage-format.directive';
import { NumberOfOuncesFormatDirective } from '../number-of-ounces-format.directive';
import { AddEditAssetModel } from '../create-edit/models/addeditasset.model';

@Component({
  selector: 'app-add-edi-tasset',
  standalone: true,
  templateUrl: './add-edit-asset.component.html',
  styleUrl: './add-edit-asset.component.css',
  imports: [FormsModule, ReactiveFormsModule, CurrencyFormatDirective, PercentFormatDirective, NumberOfOuncesFormatDirective]
})
export class AddEditAssetComponent {
  public data: any = inject(MAT_DIALOG_DATA);
  public asset: AddEditAssetModel = this.data.asset;

  public nameValidationError = signal('');
  public valueValidationError: string = '';
  public percentValidationError: string = '';
  public amountValidationError: string = '';
  public numberOfOuncesValidationError: string = '';

  constructor() {}

  isValueDisabled(): boolean {
    return this.asset.type === null && !this.asset.isPercent;
  }

  isBullion(): boolean {
    return this.asset.name.trim().toLowerCase() === 'bullion';
  }

  onNameBlur(): boolean {
    return this.validateName();
  }

  validateName(): boolean {
    if (this.asset.name.trim() === '') {
      this.nameValidationError.set('Name is required.');
      return false;
    }

    this.nameValidationError.set('');
    return true;
  }
}
