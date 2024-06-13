CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ProductTypeId] INT,
	[MatchDate] datetime NOT NULL,
	[StartTime] datetime NOT NULL,
	[EndTime] datetime NOT NULL,
	[Team1Id] int NOT NULL,
	[Team2Id] int NOT NULL,
	[FileId] INT NULL,
	[VenueId] int NOT NULL,
	[Description] nvarchar(max),
	[ScheduleDate] datetime null,
	[CreatedBy]        INT            NULL,
    [CreatedDateTime]  DATETIME       NULL,
    [UpdatedBy]        INT            NULL,
    [UpdatedDateTime]  DATETIME       NULL,
    [IsDeleted]        BIT            NOT NULL DEFAULT 0,
	CONSTRAINT [FK_Product_LogoId] FOREIGN KEY ([FileId]) REFERENCES [dbo].[File] ([Id]),
    CONSTRAINT [FK_Product_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_Product_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[User] ([Id])
)
