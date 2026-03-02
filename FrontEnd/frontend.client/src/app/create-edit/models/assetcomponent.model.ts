export class AssetComponentModel {
  constructor(
    public assetTypeId: number,
    public value: number,
    public percentage?: number,
    public name: string = ''
  ) {}
}
