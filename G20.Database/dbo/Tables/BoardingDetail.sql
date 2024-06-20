CREATE TABLE [dbo].[BoardingDetail]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	UserId INT NOT NULL,
    OrderProductItemDetailId INT NOT NULL,
    EntryDateTime DATETIME NOT NULL,
    [CreatedBy]       INT            NULL,
    [CreatedDateTime] DATETIME       NULL,
    [UpdatedBy]       INT            NULL,
    [UpdatedDateTime] DATETIME       NULL,
    CONSTRAINT [FK_BoardingDetail_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ,
    CONSTRAINT [FK_BoardingDetail_OrderProductItemDetailId] FOREIGN KEY ([OrderProductItemDetailId]) REFERENCES [dbo].[OrderProductItemDetail] ([Id])  ON DELETE CASCADE,
    CONSTRAINT [FK_BoardingDetail_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_BoardingDetail_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[User] ([Id])
)
