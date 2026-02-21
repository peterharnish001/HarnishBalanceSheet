import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-hbs-index',
  standalone: true,
  templateUrl: './hbs-index.component.html',
  styleUrl: './hbs-index.component.css'
})
export class HbsIndexComponent implements OnInit {

constructor(private http: HttpClient) {}

 ngOnInit() {
    this.http.get('http://localhost:5298/api/index/has-targets?count=1')
      .subscribe(response => {
        console.log(response);
      });
 }
}
