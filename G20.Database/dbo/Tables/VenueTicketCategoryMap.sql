CREATE TABLE [dbo].[VenueTicketCategoryMap]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [VenueId] INT NOT NULL, 
    [TicketCategoryId] INT NOT NULL, 
    [Capacity] INT NOT NULL, 
    [Amount] DECIMAL NOT NULL,
    [CreatedBy]       INT            NULL,
    [CreatedDateTime] DATETIME       NULL,
    [UpdatedBy]       INT            NULL,
    [UpdatedDateTime] DATETIME       NULL,
    [IsDeleted]       BIT            NOT NULL DEFAULT 0,
    CONSTRAINT [FK_VenueTicketCategoryMap_VenueId] FOREIGN KEY ([VenueId]) REFERENCES [dbo].[Venue] ([Id]),
    CONSTRAINT [FK_VenueTicketCategoryMap_TicketCategoryId] FOREIGN KEY ([TicketCategoryId]) REFERENCES [dbo].[TicketCategory] ([Id]),
    CONSTRAINT [FK_VenueTicketCategoryMap_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_VenueTicketCategoryMap_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[User] ([Id])
)
