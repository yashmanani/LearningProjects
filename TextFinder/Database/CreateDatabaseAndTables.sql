USE [master];
GO

CREATE DATABASE [GisMasterTranslationLogs];
GO

USE [GisMasterTranslationLogs];
GO

CREATE TABLE [MatchNotFoundExceptions]
(
	[MatchNotFoundExceptionId] INT PRIMARY KEY IDENTITY(1,1),
	[FileName] NVARCHAR(MAX) NOT NULL,
	[LineNo] INT NOT NULL,
	[Control] VARCHAR(100) NOT NULL,
	[InputText] NVARCHAR(MAX) NOT NULL,
	[TranslatedText] NVARCHAR(MAX) NOT NULL,
	[CreatedOn] DATETIME DEFAULT (GETDATE()) NOT NULL
);
GO

CREATE TABLE [FileNotExistExceptions]
(
	[FileNotExistExceptionId] INT PRIMARY KEY IDENTITY(1,1),
	[FileName] NVARCHAR(MAX) NOT NULL,
	[LineNo] INT NOT NULL,
	[Control] VARCHAR(100) NOT NULL,
	[InputText] NVARCHAR(MAX) NOT NULL,
	[TranslatedText] NVARCHAR(MAX) NOT NULL,
	[CreatedOn] DATETIME DEFAULT (GETDATE()) NOT NULL
);
GO

CREATE TABLE [SuccessfullyUpdatedLogs]
(
	[SuccessfullyUpdatedLogId] INT PRIMARY KEY IDENTITY(1,1),
	[FileName] NVARCHAR(MAX) NOT NULL,
	[LineNo] INT NOT NULL,
	[Control] VARCHAR(100) NOT NULL,
	[InputText] NVARCHAR(MAX) NOT NULL,
	[TranslatedText] NVARCHAR(MAX) NOT NULL,
	[CreatedOn] DATETIME DEFAULT (GETDATE()) NOT NULL
);
GO

CREATE TABLE [ExceptionLogs]
(
	[ExceptionLogId] INT PRIMARY KEY IDENTITY(1,1),
	[ExceptionSource] NVARCHAR(MAX),
	[ExceptionType] NVARCHAR(MAX),
	[InnerException] NVARCHAR(MAX),
	[Message] NVARCHAR(MAX),
	[StackTrace] NVARCHAR(MAX),
	[FileName] NVARCHAR(MAX) NOT NULL,
	[LineNo] INT NOT NULL,
	[Control] VARCHAR(100) NOT NULL,
	[InputText] NVARCHAR(MAX) NOT NULL,
	[TranslatedText] NVARCHAR(MAX) NOT NULL,
	[CreatedOn] DATETIME DEFAULT (GETDATE()) NOT NULL
);
GO

SELECT * FROM dbo.MatchNotFoundExceptions
SELECT * FROM dbo.ExceptionLogs
SELECT * FROM dbo.FileNotExistExceptions
SELECT * FROM dbo.SuccessfullyUpdatedLogs