CREATE TABLE [dbo].[OrderProductItemDetail]
(
	[Id]      INT           IDENTITY (1, 1) NOT NULL,
    UserId INT NOT NULL,
    OrderProductItemId INT NOT NULL,
    ProductId INT NOT NULL,
    ProductComboId INT,
    QRCode NVARCHAR(1000) NOT NULL,
    QRCodeFileId INT,
    [CreatedBy]       INT            NULL,
    [CreatedDateTime] DATETIME       NULL,
    [UpdatedBy]       INT            NULL,
    [UpdatedDateTime] DATETIME       NULL,
    CONSTRAINT [FK_OrderProductItemDetail_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ,
    CONSTRAINT [FK_OrderProductItemDetail_OrderProductItemId] FOREIGN KEY ([OrderProductItemId]) REFERENCES [dbo].[OrderProductItem] ([Id])  ON DELETE CASCADE,
    CONSTRAINT [FK_OrderProductItemDetail_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id])  ON DELETE CASCADE,
    CONSTRAINT [FK_OrderProductItemDetail_ProductComboId] FOREIGN KEY ([ProductComboId]) REFERENCES [dbo].[ProductCombo] ([Id])  ON DELETE CASCADE,
    CONSTRAINT [FK_OrderProductItemDetail_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_OrderProductItemDetail_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[User] ([Id]), 
    CONSTRAINT [FK_OrderProductItemDetail_QRCodeFileId] FOREIGN KEY ([QRCodeFileId]) REFERENCES [dbo].[File] ([Id]),
    CONSTRAINT [PK_OrderProductItemDetail] PRIMARY KEY ([Id])
)
