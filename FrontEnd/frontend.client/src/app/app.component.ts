import { Component } from '@angular/core';
import { HbsIndexComponent } from './hbs-index/hbs-index.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: true,
  styleUrl: './app.component.css',
  imports: [HbsIndexComponent]
})
export class AppComponent {
}
