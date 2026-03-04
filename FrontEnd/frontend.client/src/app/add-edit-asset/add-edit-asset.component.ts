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

  public nameValidationError: string = '';
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

  onValueBlur(): boolean {
    return this.validateValue();
  }

  onPercentBlur($event: any): boolean {
    return this.validatePercent($event);
  }

  validateName(): boolean {
    if (this.asset.name.trim() === '') {
      this.nameValidationError = 'Name is required.';
      return false;
    }

    this.nameValidationError = '';
    return true;
  }

  validateValue(): boolean {
    if ((this.asset.type !== null || this.asset.isPercent) && !this.isBullion()) {
      if (isNaN(this.asset.totalValue)) {
        this.valueValidationError = 'Value must be numeric.';
        return false;
      } else if (this.asset.totalValue === 0) {
        this.valueValidationError = 'Value must be greater than 0.';
        return false;
      }
    }

    this.valueValidationError = '';
    return true;
  }

  validatePercent($event: any): boolean {
    if (this.asset.type === null && this.asset.isPercent && !this.isBullion()) {
      const val = $event.target.value.replace(/[^\d.]/g, '');
      if (val === '' || isNaN(val)) {
        this.percentValidationError = 'Percent must be numeric.';
        return false;
      } else if (Number(val) > 100) {
        this.percentValidationError = 'Percent must not be greater than 100.';
        return false;
      }
    }

    this.percentValidationError = '';
    return true;
  }
}
