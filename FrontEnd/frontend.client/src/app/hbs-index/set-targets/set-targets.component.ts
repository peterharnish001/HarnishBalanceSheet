import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormsModule, FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { AssetTypeModel } from '../models/assettype.model';
import { TargetInputModel } from '../models/targetinput.model';

@Component({
  selector: 'app-set-targets',
  standalone: true,
  templateUrl: './set-targets.component.html',
  styleUrl: './set-targets.component.css',
  imports: [FormsModule, ReactiveFormsModule, CommonModule]
})
export class SetTargetsComponent implements OnInit {
  public data: AssetTypeModel[] = inject(MAT_DIALOG_DATA);
  public targetInputModels: TargetInputModel[] = [];
  public errMsg: string = '';
  public clicked: boolean = false;

  constructor(private fb: FormBuilder,
    private dialogRef: MatDialogRef<SetTargetsComponent>
  ) {}

  ngOnInit(): void {
   this.targetInputModels = this.data.map(datum => new TargetInputModel(datum.assetTypeId, datum.name, 0));
  }

  onClick(): void {
    this.clicked = true;
    if (this.validate()) {
      this.dialogRef.close(this.targetInputModels);
    }
  }

  validate(): boolean {
    if (this.targetInputModels.find(target => target.percentage < 0)) {
      this.errMsg = 'Percentages must be non-negative.';
      return false;
    }
    if (this.targetInputModels.find(target => target.percentage > 100))    {
      this.errMsg = 'Percentages must not be greater than 100.';
      return false;
    }
      if (this.clicked && this.targetInputModels.reduce((sum, item) => {
          const value = Number(item.percentage);
          return sum + (isNaN(value) ? 0 : value);
        }, 0) !== 100) {
          this.errMsg = 'Percentages must add up to 100.'
          return false;
        }
    this.errMsg = '';
    return true;
  }

  errorMsg(): string {
    return this.errMsg;
  }
}
