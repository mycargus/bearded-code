CREATE TABLE [dbo].[Image]
(
	[IIndex] INT NOT NULL PRIMARY KEY, 
    [CIndex] INT NOT NULL, 
    [FileName] VARCHAR(20) NOT NULL, 
    [Description] VARCHAR(20) NULL, 
    [ImageDate] DATETIME NULL, 
    [Format] VARCHAR(4) NOT NULL, 
    [Height] INT NULL, 
    [Width] INT NULL, 
    [Image] IMAGE NOT NULL, 
    CONSTRAINT [FK_Image_Category] FOREIGN KEY ([CIndex]) REFERENCES [Category]([CIndex]) 
)
