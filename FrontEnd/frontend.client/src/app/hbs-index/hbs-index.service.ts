import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environment';
import { AssetTypeModel } from './models/assettype.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HbsIndexService {
  constructor(private http: HttpClient) {}

  public getHasTargets(): Observable<AssetTypeModel[]> {
    return this.http.get<AssetTypeModel[]>(environment.apiUrl + 'index/has-targets');
  }
}
