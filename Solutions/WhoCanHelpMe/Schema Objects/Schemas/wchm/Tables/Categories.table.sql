CREATE TABLE [wchm].Categories (
    [Name]        NVARCHAR (MAX) NOT NULL,
    [SortOrder]       INT            NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [CategoryId]          INT            IDENTITY (1, 1) NOT NULL
);

