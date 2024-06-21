CREATE TABLE [dbo].[PaymentDetail]
(
	[Id]      INT           IDENTITY (1, 1) NOT NULL,
    TypeId INT NOT NULL,
    TransactionId VARCHAR(255) NOT NULL,
    GrossTotal DECIMAL(10, 2) NOT NULL,
    Discount DECIMAL(10, 2),
    GrandTotal DECIMAL(10, 2) NOT NULL,
    [CreatedBy]       INT            NULL,
    [CreatedDateTime] DATETIME       NULL,
    [UpdatedBy]       INT            NULL,
    [UpdatedDateTime] DATETIME       NULL,
    CONSTRAINT [PK_PaymentDetail] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PaymentDetail_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_PaymentDetail_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[User] ([Id])
)
