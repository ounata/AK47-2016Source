CREATE PROCEDURE [PM].[InitCategories]
AS
BEGIN

INSERT [PM].[Categories] ([CategoryID], [Category], [CategoryName], [CategoryType], [HasCourse], [Enabled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'FF81A075-09D6-4B86-97F1-F689EC7A8ACE', N'10', N'一对一', N'1', 1, 1, 99, NULL, NULL, CAST(N'2016-03-18 09:52:59.373' AS DateTime), NULL, NULL, CAST(N'2016-03-18 09:52:59.373' AS DateTime))

INSERT [PM].[Categories] ([CategoryID], [Category], [CategoryName], [CategoryType], [HasCourse], [Enabled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'61CA053D-F715-4798-A295-418FC824D838', N'20', N'班组', N'2', 1, 1, 99, NULL, NULL, CAST(N'2016-03-18 09:53:07.393' AS DateTime), NULL, NULL, CAST(N'2016-03-18 09:53:07.393' AS DateTime))

INSERT [PM].[Categories] ([CategoryID], [Category], [CategoryName], [CategoryType], [HasCourse], [Enabled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'801938F4-AF2A-4612-AC39-2A0587422F2D', N'31', N'国内游学', N'3', 0, 1, 99, NULL, NULL, CAST(N'2016-03-18 09:53:20.783' AS DateTime), NULL, NULL, CAST(N'2016-03-18 09:53:20.783' AS DateTime))

INSERT [PM].[Categories] ([CategoryID], [Category], [CategoryName], [CategoryType], [HasCourse], [Enabled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'801938F4-AF2A-4612-AC39-2A0587422F2E', N'32', N'国际游学', N'3', 0, 1, 99, NULL, NULL, CAST(N'2016-03-18 09:53:20.783' AS DateTime), NULL, NULL, CAST(N'2016-03-18 09:53:20.783' AS DateTime))

INSERT [PM].[Categories] ([CategoryID], [Category], [CategoryName], [CategoryType], [HasCourse], [Enabled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'9FA78CA1-9D48-4DF5-AA96-93F5DD0A7D48', N'41', N'实物产品', N'4', 0, 1, 99, NULL, NULL, CAST(N'2016-03-18 09:53:28.687' AS DateTime), NULL, NULL, CAST(N'2016-03-18 09:53:28.687' AS DateTime))

INSERT [PM].[Categories] ([CategoryID], [Category], [CategoryName], [CategoryType], [HasCourse], [Enabled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'41B14CE3-B3A6-48BB-B3C2-68E6CCE7679F', N'42', N'虚拟产品', N'4', 0, 1, 99, NULL, NULL, CAST(N'2016-03-18 09:54:52.067' AS DateTime), NULL, NULL, CAST(N'2016-03-18 09:54:52.067' AS DateTime))

INSERT [PM].[Categories] ([CategoryID], [Category], [CategoryName], [CategoryType], [HasCourse], [Enabled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'CA8778A1-4DE2-4EDF-BC85-956C6B7464B5', N'43', N'费用产品', N'4', 0, 1, 99, NULL, NULL, CAST(N'2016-03-18 09:55:10.503' AS DateTime), NULL, NULL, CAST(N'2016-03-18 09:55:10.503' AS DateTime))

INSERT [PM].[Categories] ([CategoryID], [Category], [CategoryName], [CategoryType], [HasCourse], [Enabled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'49FCC428-9CB1-432A-9535-BCEC5CB5B0AB', N'44', N'其它产品', N'4', 0, 1, 99, NULL, NULL, CAST(N'2016-03-18 09:55:51.783' AS DateTime), NULL, NULL, CAST(N'2016-03-18 09:55:51.783' AS DateTime))

INSERT [PM].[Categories] ([CategoryID], [Category], [CategoryName], [CategoryType], [HasCourse], [Enabled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'90C34E75-E2A7-4005-8716-3EB966F2E44B', N'50', N'无课收合作', N'5', 0, 1, 99, NULL, NULL, CAST(N'2016-03-18 09:59:01.760' AS DateTime), NULL, NULL, CAST(N'2016-03-18 09:59:01.760' AS DateTime))

END
