CREATE TABLE [wchm].Profiles (
    [ProfileId]     INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]     NVARCHAR (255) NOT NULL,
    [LastName]      NVARCHAR (255) NOT NULL,
    [UserName]      NVARCHAR (255) NOT NULL,
    [CreatedOn]     DATETIME       NOT NULL
);

