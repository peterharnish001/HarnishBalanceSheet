export class MetalPositionModel {
  constructor(
    public preciousMetalId: number,
    public metalName: string,
    public numOunces: number,
    public pricePerOunce: number,
    public totalPrice: number,
    public metalPositionId?: number,
  ) {}
}
