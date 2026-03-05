import { Component, inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-delete-liability.component',
  standalone: false,
  templateUrl: './delete-liability.component.html',
  styleUrl: './delete-liability.component.css',
})
export class DeleteLiabilityComponent {
  public data: any = inject(MAT_DIALOG_DATA);

  constructor(private dialogRef: MatDialogRef<DeleteLiabilityComponent>) {
  }

  public clickOk(): void {
    this.dialogRef.close(this.data.name);
  }

  public clickCancel(): void {
    this.dialogRef.close(null);
  }
}
