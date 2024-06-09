CREATE TABLE [dbo].[Occupation] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [ProfileId]       INT            NOT NULL,
    [Name]            NVARCHAR (MAX) NOT NULL,
    [StartDate]       DATETIME       NOT NULL,
    [EndDate]         DATETIME       NULL,
    [IsPresent]       BIT            CONSTRAINT [DF_Occupation_IsPresent] DEFAULT ((0)) NOT NULL,
    [TypeId]            INT            NOT NULL,
    [Description]     NVARCHAR (MAX) NULL,
    [CreatedBy]       INT            NULL,
    [CreatedDateTime] DATETIME       NULL,
    [UpdatedBy]       INT            NULL,
    [UpdatedDateTime] DATETIME       NULL,
    [IsDeleted]       BIT            NULL,
    [DisplayOrder]    INT            NULL,
    CONSTRAINT [PK_Occupation] PRIMARY KEY CLUSTERED ([Id] ASC)
);

