CREATE TABLE [dbo].[Venue]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[StadiumName] nvarchar(max) NOT NULL,
	[Location] nvarchar(max) NULL,
	[CountryId] INT NOT NULL,
	[Capacity] INT NULL,
	[CreatedBy] int NULL,
	[CreatedDateTime] datetime NULL,
	[UpdatedBy] int NULL,
	[UpdatedDateTime] datetime NULL,
	[IsDeleted] bit NOT NULL DEFAULT 0,
	CONSTRAINT [FK_Venue_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_Venue_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[User] ([Id]),
	CONSTRAINT [FK_Venue_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country] ([Id])
)
