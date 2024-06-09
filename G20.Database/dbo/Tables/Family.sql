CREATE TABLE [dbo].[Family] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [ProfileId]       INT            NOT NULL,
    [Name]            NVARCHAR (MAX) NOT NULL,
    [RelationTypeId]  INT            NOT NULL,
    [Email]           NVARCHAR (MAX) NULL,
    [PhoneNo]         NVARCHAR (MAX) NULL,
    [Description]     NVARCHAR (MAX) NULL,
    [CreatedBy]       INT            NULL,
    [CreatedDateTime] DATETIME       NULL,
    [UpdatedBy]       INT            NULL,
    [UpdatedDateTime] DATETIME       NULL,
    [IsDeleted]       BIT            NULL,
    [DisplayOrder]    INT            NULL,
    CONSTRAINT [PK_Family] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Family_AspNetUsers] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Family_AspNetUsers1] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Family_Profile] FOREIGN KEY ([ProfileId]) REFERENCES [dbo].[Profile] ([Id])
);

