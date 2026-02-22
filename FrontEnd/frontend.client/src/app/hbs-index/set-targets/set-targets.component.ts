import { Component, inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormsModule, FormGroup, ValidatorFn } from '@angular/forms';
import { AssetTypeModel } from '../models/assettype.model';
import { TargetInputModel } from '../models/targetinput.model';

@Component({
  selector: 'app-set-targets',
  standalone: true,
  templateUrl: './set-targets.component.html',
  styleUrl: './set-targets.component.css',
  imports: [FormsModule]
})
export class SetTargetsComponent implements OnInit {
  public dialogRef = MatDialogRef<SetTargetsComponent>;
  public data: AssetTypeModel[] = inject(MAT_DIALOG_DATA);
  public targets: TargetInputModel[] = [];
  public myForm: FormGroup;
  public isClicked: boolean = false;

  constructor(private fb: FormBuilder) {
    this.myForm = this.fb.group({
      name: ['']
    });
  }

  ngOnInit(): void {
    this.targets = this.data.map(datum => new TargetInputModel(datum.assetTypeId, datum.name, 0));
  }

  onClick(): void {
    this.isClicked = true;
  }

  sumPercentages(): number {
    return this.targets.reduce((total, obj) => {
        const value = obj['percentage'];
        if (typeof value === "number" && !isNaN(value)) {
            return total + value;
        }
        return total;
    }, 0);
  }

  public validationMsg(): string {
    if (this.targets.find((target) => target.percentage !== undefined && target.percentage < 0)) {
      return 'Percentages must be non-negative.';
    } else if (this.sumPercentages() !== 100 && this.isClicked) {
      return 'Percentages must sum to 100.'
    }

    return '';
  }

  public onTargetChange(event: any) {
    this.isClicked = false;
  }

}
