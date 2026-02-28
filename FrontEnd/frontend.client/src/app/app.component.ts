import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
//import { HbsIndexComponent } from './hbs-index/hbs-index.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: true,
  styleUrl: './app.component.css',
  imports: [RouterOutlet]
})
export class AppComponent {
}
