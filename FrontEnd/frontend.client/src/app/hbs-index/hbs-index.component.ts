import { Component, OnInit } from '@angular/core';
import { DataTablesModule } from 'angular-datatables';
import { HttpClient } from '@angular/common/http';
import DataTables from 'datatables.net';

@Component({
  selector: 'app-hbs-index',
  standalone: true,
  templateUrl: './hbs-index.component.html',
  styleUrl: './hbs-index.component.css',
  imports: [ DataTablesModule ]
})
export class HbsIndexComponent implements OnInit {
 dtOptions: DataTables.Settings = {};

constructor(private http: HttpClient) {}

 ngOnInit() {
  this.dtOptions = {
      ajax: 'http://localhost:5298/api/index/balancesheets?count=1',
      pagingType: 'full_numbers',
      pageLength: 24,
      processing: true,
      responsive: true,
      order: [[1, 'desc']],
      columnDefs: [
        { targets: [1], orderable: false }
      ]
    };

    this.http.get('http://localhost:5298/api/index/balancesheets?count=1')
      .subscribe(response => {
        console.log(response);
      });
 }
}
