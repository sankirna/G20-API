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