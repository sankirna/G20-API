/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
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
