
IF NOT EXISTS (SELECT * FROM [ROLE] WHERE NAME='ADMIN')
BEGIN
   INSERT INTO [DBO].[ROLE]
           ([NAME]
           )
     VALUES
           ('ADMIN')
END


IF NOT EXISTS (SELECT * FROM [ROLE] WHERE NAME='USER')
BEGIN
   INSERT INTO [DBO].[ROLE]
           ([NAME]
           )
     VALUES
           ('USER')
END

IF NOT EXISTS (SELECT * FROM [ROLE] WHERE NAME='AFFILIATE')
BEGIN
   INSERT INTO [DBO].[ROLE]
           ([NAME]
           )
     VALUES
           ('AFFILIATE')
END

IF NOT EXISTS (SELECT * FROM [ROLE] WHERE NAME='SECURITY')
BEGIN
   INSERT INTO [DBO].[ROLE]
           ([NAME]
           )
     VALUES
           ('SECURITY')
END


IF NOT EXISTS (SELECT * FROM [USER] WHERE [Email]='admin@g20.com')
BEGIN

 INSERT INTO [dbo].[User]
           ([UserName]
           ,[Email]
           ,[PhoneNumber]
           ,[Password]
           ,[CreatedDateTime]
           ,[IsDeleted])
     VALUES
           ('admin@g20.com'
           ,'admin'
           ,'999999999'
           ,'admin@123'
           ,GETDATE()
           ,0)

   DECLARE @userId INT;
   SET @userId=SCOPE_IDENTITY();

   INSERT INTO [UserRole]([UserId],[RoleId])
		SELECT SCOPE_IDENTITY(),Id FROM [Role]
		WHERE [Name] in ('ADMIN'); 
END



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

IF NOT EXISTS (SELECT * FROM [TicketCategory] WHERE [Name]='Gardern')
BEGIN
        INSERT INTO [dbo].[TicketCategory]
                   ([Name]
                   ,[Description]
                   ,[CreatedDateTime]
                   ,[IsDeleted])
             VALUES
                   ('Gardern'
                   ,'Gardern'
                   ,GETDATE()
                   ,0);

END

IF NOT EXISTS (SELECT * FROM [TicketCategory] WHERE [Name]='Box')
BEGIN
        INSERT INTO [dbo].[TicketCategory]
                   ([Name]
                   ,[Description]
                   ,[CreatedDateTime]
                   ,[IsDeleted])
             VALUES
                   ('Box'
                   ,'Box'
                   ,GETDATE()
                   ,0);

END


SET IDENTITY_INSERT [dbo].[EmailAccount] ON 
GO
IF NOT EXISTS (SELECT * FROM [dbo].[EmailAccount] WHERE [Id]=1)
BEGIN
        INSERT INTO [dbo].[EmailAccount]
                   ([Id]
		           ,[DisplayName]
                   ,[Email]
                   ,[Host]
                   ,[Username]
                   ,[Password]
                   ,[Port]
                   ,[EnableSsl]
                   ,[MaxNumberOfEmails]
                   ,[EmailAuthenticationMethodId])
             VALUES
                   (1
		           ,'G20'
                   ,'xxx@gmail.com'
                   ,'smtp.gmail.com'
                   ,'xxx@gmail.com'
                   ,'xxx'
                   ,'587'
                   ,0
                   ,1000
                   ,0)

		   END
SET IDENTITY_INSERT [dbo].[EmailAccount] OFF

SET IDENTITY_INSERT [dbo].[ScheduleTask] ON 
IF NOT EXISTS (SELECT * FROM [dbo].[ScheduleTask] WHERE [Id]=1)
BEGIN
    INSERT [dbo].[ScheduleTask] ([Id], [Name], [Type], [Seconds], [LastEnabledUtc], [Enabled], [StopOnError], [LastStartUtc], [LastEndUtc], [LastSuccessUtc])
    VALUES (1, N'Send emails', N'G20.Service.Messages.QueuedMessagesSendTask, G20.Service', 60, CAST(N'2024-04-29T17:40:09.6200000' AS DateTime2), 1, 0, CAST(N'2024-06-18T17:48:27.5460000' AS DateTime2), CAST(N'2024-06-18T17:47:53.9230000' AS DateTime2), CAST(N'2024-06-18T17:47:53.9230000' AS DateTime2))
END
SET IDENTITY_INSERT [dbo].[ScheduleTask] OFF
