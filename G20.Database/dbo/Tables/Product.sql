﻿CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] nvarchar(max) NOT NULL,
	[ProductTypeId] INT,
	[VenueId] int  NULL,
	[Team1Id] int  NULL,
	[Team2Id] int  NULL,
	[StartDateTime] datetime NULL,
	[EndDateTime] datetime NULL,
	[FileId] INT NULL,
	[Description] nvarchar(max),
	[ScheduleDateTime] datetime null,
	[CreatedBy]        INT            NULL,
    [CreatedDateTime]  DATETIME       NULL,
    [UpdatedBy]        INT            NULL,
    [UpdatedDateTime]  DATETIME       NULL,
    [IsDeleted]        BIT            NOT NULL DEFAULT 0,
	CONSTRAINT [FK_Product_VenueId] FOREIGN KEY ([VenueId]) REFERENCES [dbo].[Venue] ([Id]),
	CONSTRAINT [FK_Product_Team1Id] FOREIGN KEY ([Team1Id]) REFERENCES [dbo].[Team] ([Id]),
	CONSTRAINT [FK_Product_Team2Id] FOREIGN KEY ([Team2Id]) REFERENCES [dbo].[Team] ([Id]),
	CONSTRAINT [FK_Product_FileId] FOREIGN KEY ([FileId]) REFERENCES [dbo].[File] ([Id]),
    CONSTRAINT [FK_Product_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_Product_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[User] ([Id])
)
