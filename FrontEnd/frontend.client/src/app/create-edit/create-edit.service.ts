import { Injectable, signal } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { environment } from '../environment';
import { Observable } from 'rxjs';
import { AssetModel } from './models/asset.model';
import { BalanceSheetModel } from './models/balancesheet.model';

@Injectable({
  providedIn: 'root',
})
export class CreateEditService {
  private readonly _balanceSheet = signal<BalanceSheetModel | null>(null);
  private readonly _assets = signal<AssetModel[]>([]);

  public readonly balanceSheet = this._balanceSheet.asReadonly();
  public readonly assets = this._assets.asReadonly();

  constructor(private http: HttpClient) {}

  public getCurrent(): void {
    this.http.get<BalanceSheetModel>(environment.apiUrl + 'balance-sheet/create')
      .subscribe((result: BalanceSheetModel) => {
        this._balanceSheet.set(result);
        this._assets.set(result.assets);
      });
  }
}
