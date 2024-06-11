CREATE TABLE [dbo].[StandCategory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [StandName] NVARCHAR(MAX) NOT NULL, 
    [VenueId] INT NULL,
    [Capacity] INT NULL,
    [EntryGate] NVARCHAR(MAX) NULL,
    [Facilities] NVARCHAR(MAX) NULL,
    [CreatedBy]        INT            NULL,
    [CreatedDateTime]  DATETIME       NULL,
    [UpdatedBy]        INT            NULL,
    [UpdatedDateTime]  DATETIME       NULL,
    [IsDeleted]        BIT            NOT NULL DEFAULT 0,
    CONSTRAINT [FK_StandCategory_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_StandCategory_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[User] ([Id])
)
