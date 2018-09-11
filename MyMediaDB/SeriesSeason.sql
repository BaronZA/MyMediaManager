﻿CREATE TABLE [dbo].[SeriesSeason]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[SeriesID] INT NOT NULL,
	[SeasonNumber] INT NOT NULL,
	[StorageLocation] VARCHAR(255) NOT NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC),
	FOREIGN KEY (SeriesID) REFERENCES Series(ID)
)