import { AssetComponentModel } from "./assetcomponent.model";
import { MetalPositionModel } from "./metalposition.model";

export class AddEditAssetModel {
  constructor(
    public name: string = '',
    public type: number | null = null,
    public isPercent: boolean = false,
    public totalValue: number = 0,
    public components: AssetComponentModel[] = [],
    public bullion: MetalPositionModel[] = []
  ) {}
}
