import { Component, OnInit, inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { HbsIndexService } from './hbs-index.service';
import { SetTargetsComponent } from './set-targets/set-targets.component';

@Component({
  selector: 'app-hbs-index',
  standalone: true,
  templateUrl: './hbs-index.component.html',
  styleUrl: './hbs-index.component.css'
})
export class HbsIndexComponent implements OnInit {
  private dialog = inject(MatDialog);

  constructor(private service: HbsIndexService) {}

 ngOnInit() {
  this.service.getHasTargets()
    .subscribe({
      next: (output) => {
        if (output.length > 0) {
          this.dialog.open(SetTargetsComponent, {
            data: output
          });
        }
      }
    });
 }
}
