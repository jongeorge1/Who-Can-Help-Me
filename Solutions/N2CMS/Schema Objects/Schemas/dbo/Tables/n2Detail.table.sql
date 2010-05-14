CREATE TABLE [dbo].[n2Detail] (
    [ID]                 INT             IDENTITY (1, 1) NOT NULL,
    [Type]               NVARCHAR (255)  NOT NULL,
    [ItemID]             INT             NOT NULL,
    [DetailCollectionID] INT             NULL,
    [Name]               NVARCHAR (50)   NULL,
    [BoolValue]          BIT             NULL,
    [IntValue]           INT             NULL,
    [LinkValue]          INT             NULL,
    [DoubleValue]        FLOAT           NULL,
    [DateTimeValue]      DATETIME        NULL,
    [StringValue]        NVARCHAR (MAX)  NULL,
    [Value]              VARBINARY (MAX) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

