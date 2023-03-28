USE [master]
GO

DECLARE @kill varchar(8000) = ''
SELECT @kill = @kill + 'kill ' + CONVERT(varchar(5), session_id) + ';'
FROM sys.dm_exec_sessions
WHERE database_id = DB_ID('Door2DoorDB')

EXEC(@kill)
GO

DROP DATABASE IF EXISTS [Door2DoorDB]
GO

CREATE DATABASE [Door2DoorDB]
GO

USE [Door2DoorDB]
GO

/*##################################################
				## Drop Tables ##
####################################################*/
DROP TABLE IF EXISTS [locations];
DROP TABLE IF EXISTS [routes];
DROP TABLE IF EXISTS [admin];
DROP TABLE IF EXISTS [log];
DROP TABLE IF EXISTS [logTypes];


/*####################################################
			## Create Tables ##
####################################################*/

CREATE TABLE [locations] (
[id] BIGINT NOT NULL IDENTITY,
[name] VARCHAR(255) not null,
[iconUrl] VARCHAR(255) DEFAULT ' ')
GO

CREATE TABLE [routes]
(
[id] BIGINT NOT NULL IDENTITY,
[startLocation] BIGINT NOT NULL,
[endLocation] BIGINT NOT NULL,
[text] VARCHAR(255) NOT NULL,
[videoUrl] VARCHAR(255) NOT NULL
)
GO

CREATE TABLE [admin](
[id] BIGINT NOT NULL IDENTITY,
[username] VARCHAR(255) NOT NULL ,
[password] VARCHAR(255) NOT NULL) 
GO

CREATE TABLE [log](
[id] BIGINT NOT NULL IDENTITY,
[type] BIGINT NOT NULL ,
[description] VARCHAR(255) NOT NULL,
[timestamp] DATETIME DEFAULT GETDATE())
GO

CREATE TABLE [logTypes](
[id] BIGINT NOT NULL IDENTITY,
[errorCodes] VARCHAR(255) NOT NULL)
GO

/*####################################################
			## Alter Tables ##
####################################################*/

ALTER TABLE [locations]
ADD
	PRIMARY KEY ([id])
GO

ALTER TABLE [routes]
	ADD PRIMARY KEY ([id])
GO

ALTER TABLE [routes] 
ADD
	CONSTRAINT fk_routes_startLocation FOREIGN KEY ([startLocation]) REFERENCES [locations] ([id]) ON UPDATE CASCADE ON DELETE NO ACTION
GO

ALTER TABLE [admin]
ADD
	PRIMARY KEY ([id])
GO

ALTER TABLE [logTypes]
ADD
	PRIMARY KEY ([id])
GO

ALTER TABLE [log] 
ADD
	PRIMARY KEY ([id]),
	FOREIGN KEY ([type]) REFERENCES [logTypes] ([id])
GO
