export class MetalPositionModel {
  constructor(
    public metalPositionId: number,
    public preciousMetalId: number,
    public metalName: string,
    public numOunces: number,
    public pricePerOunce: number,
    public totalPrice: number
  ) {}
}
