import { Component, inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AssetTypeModel } from '../models/assettype.model';

@Component({
  selector: 'app-set-targets',
  standalone: false,
  templateUrl: './set-targets.component.html',
  styleUrl: './set-targets.component.css',
})
export class SetTargetsComponent {
  public dialogRef = MatDialogRef<SetTargetsComponent>;
  public data: AssetTypeModel[] = inject(MAT_DIALOG_DATA);
}
