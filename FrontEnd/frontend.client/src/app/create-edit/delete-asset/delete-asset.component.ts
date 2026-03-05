import { Component, inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-delete-asset.component',
  standalone: false,
  templateUrl: './delete-asset.component.html',
  styleUrl: './delete-asset.component.css',
})
export class DeleteAssetComponent {
  public data: any = inject(MAT_DIALOG_DATA);

  constructor(private dialogRef: MatDialogRef<DeleteAssetComponent>) {
  }

  public clickOk(): void {
    this.dialogRef.close(this.data.name);
  }

  public clickCancel(): void {
    this.dialogRef.close(null);
  }
}
