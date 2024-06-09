CREATE TABLE [dbo].[MigrationVersionInfo] (
    [Version]     BIGINT          NOT NULL,
    [AppliedOn]   DATETIME        NULL,
    [Description] NVARCHAR (1024) NULL
);


GO
CREATE UNIQUE CLUSTERED INDEX [UC_Version]
    ON [dbo].[MigrationVersionInfo]([Version] ASC);

