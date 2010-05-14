CREATE TABLE [dbo].[n2Item] (
    [ID]             INT            IDENTITY (1, 1) NOT NULL,
    [Type]           NVARCHAR (255) NOT NULL,
    [Created]        DATETIME       NOT NULL,
    [Published]      DATETIME       NULL,
    [Updated]        DATETIME       NOT NULL,
    [Expires]        DATETIME       NULL,
    [Name]           NVARCHAR (255) NULL,
    [ZoneName]       NVARCHAR (50)  NULL,
    [Title]          NVARCHAR (255) NULL,
    [SortOrder]      INT            NOT NULL,
    [Visible]        BIT            NOT NULL,
    [SavedBy]        NVARCHAR (255)  NULL,
    [State]          INT            NULL,
    [AncestralTrail] NVARCHAR (100) NULL,
    [VersionIndex]   INT            NULL,
    [VersionOfID]    INT            NULL,
    [ParentID]       INT            NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

