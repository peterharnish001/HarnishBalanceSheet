import { AssetComponentModel } from "./assetcomponent.model";

export class AddEditAssetModel {
  constructor(
    public name: string = '',
    public type: number | null = null,
    public isPercent: boolean = false,
    public totalValue: number = 0,
    public components: AssetComponentModel[] = []
  ) {}
}
