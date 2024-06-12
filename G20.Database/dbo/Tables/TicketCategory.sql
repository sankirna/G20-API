CREATE TABLE [dbo].[TicketCategory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [Description] NVARCHAR(MAX)  NULL, 
    [FileId] INT NULL,
    [CreatedBy]        INT            NULL,
    [CreatedDateTime]  DATETIME       NULL,
    [UpdatedBy]        INT            NULL,
    [UpdatedDateTime]  DATETIME       NULL,
    [IsDeleted]        BIT            NOT NULL DEFAULT 0,
    CONSTRAINT [FK_TicketCategory_LogoId] FOREIGN KEY ([FileId]) REFERENCES [dbo].[File] ([Id]),
    CONSTRAINT [FK_TicketCategory_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_TicketCategory_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[User] ([Id])
)
