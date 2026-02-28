import { Component } from '@angular/core';
import { DatePipe } from '@angular/common';
import { CreateEditComponent } from '../create-edit/create-edit.component'

@Component({
  selector: 'app-create',
  standalone: true,
  templateUrl: './create.component.html',
  styleUrl: './create.css',
  imports: [CreateEditComponent, DatePipe]
})
export class CreateComponent {
  public today: Date = new Date();
}
