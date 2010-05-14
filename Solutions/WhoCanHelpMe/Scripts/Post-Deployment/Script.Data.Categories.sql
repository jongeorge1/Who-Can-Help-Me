SET IDENTITY_INSERT [wchm].Categories ON
GO

INSERT INTO [wchm].Categories (CategoryId, [Name], [SortOrder], Description) VALUES
 (1, 'Has previously worked for', 7, 'Use for listing companies you have worked for')
,(2, 'Is a', 1, 'Use for listing your level / position')
,(3, 'Has worked for', 6, 'Use for listing clients you have worked for')
,(4, 'Is a member of the', 9, 'Use for listing communities you belong to')
,(5, 'Has attended', 12, 'Use for listing conferences you have attended')
,(6, 'Knows about', 3, 'Use for listing topics you know about')
,(7, 'Belongs to the', 2, 'Use for the name of the team you work for')
,(8, 'Is interested in', 4, 'Use for listing topics you are interested in')
,(9, 'Has worked on', 8, 'Use for listing clients you have worked for')
,(10, 'Has training for', 11, 'Use for listing training courses you have attended')
,(11, 'Has experience in', 5, 'Use for listing experiences in business verticals')
,(12, 'Is certified as a', 10, 'Use for listing certifications')
,(13, 'Blogs at', 13, 'Use for your blog Url')
,(14, 'Tweets as', 14, 'Use for your Twitter handle')
GO

SET IDENTITY_INSERT [wchm].Categories OFF
GO
