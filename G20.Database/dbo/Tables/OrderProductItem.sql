CREATE TABLE [dbo].[OrderProductItem]
(
	[Id]      INT           IDENTITY (1, 1) NOT NULL,
    UserId INT NOT NULL,
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    ProductTypeId INT NOT NULL,
    ProductTicketCategoryMapId INT NOT NULL,
    Quantity INT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    TotalPrice DECIMAL(10, 2) NOT NULL,
    [CreatedBy]       INT            NULL,
    [CreatedDateTime] DATETIME       NULL,
    [UpdatedBy]       INT            NULL,
    [UpdatedDateTime] DATETIME       NULL,
    CONSTRAINT [FK_OrderProductItem_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ,
    CONSTRAINT [FK_OrderProductItem_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([Id]) ON DELETE CASCADE ,
    CONSTRAINT [FK_OrderProductItem_ProductTicketCategoryMapId] FOREIGN KEY ([ProductTicketCategoryMapId]) REFERENCES [dbo].[ProductTicketCategoryMap] ([Id]) ON DELETE CASCADE ,
    CONSTRAINT [FK_OrderProductItem_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_OrderProductItem_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[User] ([Id]), 
    CONSTRAINT [PK_OrderProductItem] PRIMARY KEY ([Id])
)
