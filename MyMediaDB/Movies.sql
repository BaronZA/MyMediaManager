CREATE TABLE [dbo].[Movie]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[Name] VARCHAR (50) NOT NULL,
	[StorageLocation] VARCHAR(255) NOT NULL,
	[ReleaseDate] DATETIME NULL,
	[Genre] VARCHAR(30) NULL,
	[RunTimeMinutes] INT NULL,
	[Budget] DECIMAL NULL,
	[Revenue] DECIMAL NULL,
	[HomePage] VARCHAR(255) NULL,
	[Overview] VARCHAR(MAX) NULL,
	[CastDetails] VARCHAR(MAX) NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC),
)