import { AssetComponentModel } from "./assetcomponent.model";

export class AssetModel {
  constructor(
    public name: string,
    public totalValue: number,
    public assetComponents: AssetComponentModel[],
    public assetId?: number,
    public isPercent?: boolean
  ) {}
}
