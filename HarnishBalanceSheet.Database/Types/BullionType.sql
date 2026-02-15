CREATE TYPE dbo.BullionType AS TABLE
(
    MetalPositionId INT,
    PreciousMetalId INT NOT NULL,
    NumOunces DECIMAL NOT NULL,
    PricePerOunce DECIMAL(15,2) NOT NULL
);