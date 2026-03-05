import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LiabilityModel } from '../models/liability.model';
import { CurrencyFormatDirective } from '../../currency-format.directive';

@Component({
  selector: 'app-add-liability.component',
  standalone: true,
  templateUrl: './add-liability.component.html',
  styleUrl: './add-liability.component.css',
  imports: [FormsModule, ReactiveFormsModule, CurrencyFormatDirective]
})
export class AddLiabilityComponent {
  public liability: LiabilityModel = new LiabilityModel();
}
