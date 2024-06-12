
IF NOT EXISTS (SELECT * FROM [TicketCategory] WHERE [Name]='Gold')
BEGIN
        INSERT INTO [dbo].[TicketCategory]
                   ([Name]
                   ,[Description]
                   ,[CreatedDateTime]
                   ,[IsDeleted])
             VALUES
                   ('Gold'
                   ,'Gold'
                   ,GETDATE()
                   ,0);

END

IF NOT EXISTS (SELECT * FROM [TicketCategory] WHERE [Name]='Silver')
BEGIN
        INSERT INTO [dbo].[TicketCategory]
                   ([Name]
                   ,[Description]
                   ,[CreatedDateTime]
                   ,[IsDeleted])
             VALUES
                   ('Silver'
                   ,'Silver'
                   ,GETDATE()
                   ,0);

END

IF NOT EXISTS (SELECT * FROM [TicketCategory] WHERE [Name]='Platinum')
BEGIN
        INSERT INTO [dbo].[TicketCategory]
                   ([Name]
                   ,[Description]
                   ,[CreatedDateTime]
                   ,[IsDeleted])
             VALUES
                   ('Platinum'
                   ,'Platinum'
                   ,GETDATE()
                   ,0);

END