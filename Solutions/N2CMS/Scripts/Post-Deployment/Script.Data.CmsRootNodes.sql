-- =============================================
-- Script Template
-- =============================================
SET NUMERIC_ROUNDABORT OFF
GO
SET XACT_ABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
/*Pointer used for text / image updates. This might not be needed, but is declared here just in case*/
DECLARE @pv binary(16)
BEGIN TRANSACTION
ALTER TABLE [dbo].[n2Detail] DROP CONSTRAINT [FK1D14F83B3AF5DAB0]
ALTER TABLE [dbo].[n2Detail] DROP CONSTRAINT [FK1D14F83B4F9855AA]
ALTER TABLE [dbo].[n2Detail] DROP CONSTRAINT [FK1D14F83B6AB29607]
ALTER TABLE [dbo].[n2DetailCollection] DROP CONSTRAINT [FKBE85C49A6AB29607]
ALTER TABLE [dbo].[n2AllowedRole] DROP CONSTRAINT [FKB30F0676AB29607]
ALTER TABLE [dbo].[n2Item] DROP CONSTRAINT [FK18406FA018DD5AFD]
ALTER TABLE [dbo].[n2Item] DROP CONSTRAINT [FK18406FA04B1A4E60]
SET IDENTITY_INSERT [dbo].[n2Item] ON
INSERT INTO [dbo].[n2Item] ([ID], [Type], [Created], [Published], [Updated], [Expires], [Name], [ZoneName], [Title], [SortOrder], [Visible], [SavedBy], [State], [AncestralTrail], [VersionIndex], [VersionOfID], [ParentID]) VALUES (1, N'SiteRoot', '20100118 15:48:59.000', '20100118 15:48:59.000', '20100118 15:48:59.000', NULL, N'root', NULL, N'Who Can Help Me?', 0, 1, N'admin', 1, N'/', 0, NULL, NULL)
INSERT INTO [dbo].[n2Item] ([ID], [Type], [Created], [Published], [Updated], [Expires], [Name], [ZoneName], [Title], [SortOrder], [Visible], [SavedBy], [State], [AncestralTrail], [VersionIndex], [VersionOfID], [ParentID]) VALUES (2, N'HomePage', '20100118 15:48:59.000', '20100118 15:48:59.000', '20100127 10:03:56.000', NULL, N'home', NULL, N'Home', 0, 1, N'admin', 16, N'/1/', 1, NULL, 1)
SET IDENTITY_INSERT [dbo].[n2Item] OFF
ALTER TABLE [dbo].[n2Detail] ADD CONSTRAINT [FK1D14F83B3AF5DAB0] FOREIGN KEY ([LinkValue]) REFERENCES [dbo].[n2Item] ([ID])
ALTER TABLE [dbo].[n2Detail] ADD CONSTRAINT [FK1D14F83B4F9855AA] FOREIGN KEY ([DetailCollectionID]) REFERENCES [dbo].[n2DetailCollection] ([ID])
ALTER TABLE [dbo].[n2Detail] ADD CONSTRAINT [FK1D14F83B6AB29607] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[n2Item] ([ID])
ALTER TABLE [dbo].[n2DetailCollection] ADD CONSTRAINT [FKBE85C49A6AB29607] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[n2Item] ([ID])
ALTER TABLE [dbo].[n2AllowedRole] ADD CONSTRAINT [FKB30F0676AB29607] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[n2Item] ([ID])
ALTER TABLE [dbo].[n2Item] ADD CONSTRAINT [FK18406FA018DD5AFD] FOREIGN KEY ([ParentID]) REFERENCES [dbo].[n2Item] ([ID])
ALTER TABLE [dbo].[n2Item] ADD CONSTRAINT [FK18406FA04B1A4E60] FOREIGN KEY ([VersionOfID]) REFERENCES [dbo].[n2Item] ([ID])
COMMIT TRANSACTION
