CREATE TABLE [dbo].[Team]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [ShortName] NVARCHAR(MAX) NULL, 
    [CountryId] INT NOT NULL, 
    [StateId] INT NULL, 
    [CityId] INT NULL, 
    [FoundedYear] INT NULL, 
    [ManagerName] NVARCHAR(MAX) NULL, 
    [LogoId] INT NULL, 
    [Colors] NVARCHAR(50) NULL,
    [IsDeleted]        BIT            NOT NULL DEFAULT 0,
    [CreatedBy]        INT            NULL,
    [CreatedDateTime]  DATETIME       NULL,
    [UpdatedBy]        INT            NULL,
    [UpdatedDateTime]  DATETIME       NULL,
    CONSTRAINT [FK_Team_Country] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country] ([Id]),
    CONSTRAINT [FK_Team_City] FOREIGN KEY ([CityId]) REFERENCES [dbo].[City] ([Id]),
    CONSTRAINT [FK_Team_LogoId] FOREIGN KEY ([LogoId]) REFERENCES [dbo].[File] ([Id]),
    CONSTRAINT [FK_Team_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_Team_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[User] ([Id])
)
