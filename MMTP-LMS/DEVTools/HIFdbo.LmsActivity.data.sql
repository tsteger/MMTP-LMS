SET IDENTITY_INSERT [dbo].[LmsActivity] ON
INSERT INTO [dbo].[LmsActivity] ([Id], [Name], [Description], [StartDate], [EndTime], [LmsActivityTypeId], [ModuleId]) VALUES (1, N'Dribble and Hurl', N'Finding our way around Visual Studio', N'2019-03-05 00:00:00', N'2021-03-05 00:00:00', 2, 1)
INSERT INTO [dbo].[LmsActivity] ([Id], [Name], [Description], [StartDate], [EndTime], [LmsActivityTypeId], [ModuleId]) VALUES (4, N'Fooling around', N'Fooling around with some simple code', N'2019-03-06 00:00:00', N'2019-03-06 00:00:00', 1, 2)
INSERT INTO [dbo].[LmsActivity] ([Id], [Name], [Description], [StartDate], [EndTime], [LmsActivityTypeId], [ModuleId]) VALUES (5, N'Messing With Messages', N'Writing useful messages', N'2019-03-07 00:00:00', N'2019-03-07 00:00:00', 3, 3)
INSERT INTO [dbo].[LmsActivity] ([Id], [Name], [Description], [StartDate], [EndTime], [LmsActivityTypeId], [ModuleId]) VALUES (6, N'Understanding code', N'Do you get it?', N'2019-03-08 00:00:00', N'2019-03-08 00:00:00', 4, 4)
SET IDENTITY_INSERT [dbo].[LmsActivity] OFF
