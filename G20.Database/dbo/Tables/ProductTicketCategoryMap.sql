CREATE TABLE [dbo].[ProductTicketCategoryMap]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[ProductId] INT NOT NULL,
	[TicketCategoryId] INT NOT NULL,
    [Total] INT NOT NULL,
    [Available] INT NOT NULL,
    [Block] INT NOT NULL,
    [Sold] INT NOT NULL,
    [Price] INT NOT NULL,
	[CreatedBy]        INT            NULL,
    [CreatedDateTime]  DATETIME       NULL,
    [UpdatedBy]        INT            NULL,
    [UpdatedDateTime]  DATETIME       NULL,
    [IsDeleted]        BIT            NOT NULL DEFAULT 0,
    CONSTRAINT [FK_ProductTicketCategoryMap_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Venue] ([Id]),
    CONSTRAINT [FK_ProductTicketCategoryMap_TicketCategoryId] FOREIGN KEY ([TicketCategoryId]) REFERENCES [dbo].[TicketCategory] ([Id]),
    CONSTRAINT [FK_ProductTicketCategoryMap_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_ProductTicketCategoryMap_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[User] ([Id])
)
