import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { environment } from '../environment';
import { Observable } from 'rxjs';
import { DetailsModel } from './models/details.model';

@Injectable({
  providedIn: 'root',
})
export class DetailsService {
  constructor(private http: HttpClient) {}

  public getDetails(balanceSheetId: number): Observable<DetailsModel> {
      return this.http.get<DetailsModel>(environment.apiUrl + 'balance-sheet/details/' + balanceSheetId.toString());
    }
}
