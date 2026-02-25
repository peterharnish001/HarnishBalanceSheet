import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { environment } from '../environment';
import { AssetTypeModel } from './models/assettype.model';
import { Observable } from 'rxjs';
import { SetTargetModel } from './models/settarget.model';
import { BalanceSheetDateModel } from './models/balancesheetdate.model';

@Injectable({
  providedIn: 'root',
})
export class HbsIndexService {
  constructor(private http: HttpClient) {}

  public getHasTargets(): Observable<AssetTypeModel[]> {
    return this.http.get<AssetTypeModel[]>(environment.apiUrl + 'index/has-targets');
  }

  public setTargets(targets: SetTargetModel[]): Observable<HttpResponse<any>> {
    return this.http.post<number>(environment.apiUrl + 'index/set-targets', targets, { observe: 'response' });
  }

  public getBalanceSheetData(count: number): Observable<BalanceSheetDateModel[]> {
    return this.http.get<BalanceSheetDateModel[]>(environment.apiUrl + 'index/balancesheets?count=' + count.toString());
  }
}
