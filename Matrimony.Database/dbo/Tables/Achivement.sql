CREATE TABLE [dbo].[Achivement] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [ProfileId]       INT            NOT NULL,
    [Name]            NVARCHAR (MAX) NOT NULL,
    [Year]            INT            NOT NULL,
    [Description]     NVARCHAR (MAX) NOT NULL,
    [DisplayOrder]    INT            NULL,
    [CreatedBy]       INT            NULL,
    [CreatedDateTime] DATETIME       NULL,
    [UpdatedBy]       INT            NULL,
    [UpdatedDateTime] DATETIME       NULL,
    [IsDeleted]       BIT            NULL,
    CONSTRAINT [PK_Achivement] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Achivement_AspNetUsers] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Achivement_AspNetUsers1] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Achivement_Profile] FOREIGN KEY ([ProfileId]) REFERENCES [dbo].[Profile] ([Id])
);

