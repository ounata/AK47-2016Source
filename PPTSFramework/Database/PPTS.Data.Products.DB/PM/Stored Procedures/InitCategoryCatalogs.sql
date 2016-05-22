﻿CREATE PROCEDURE [PM].[InitCategoryCatalogs]
AS
BEGIN

INSERT [PM].[CategoryCatalogs] ([CategoryID], [CatalogID], [Catalog], [CatalogName], [HasPartner], [Eanbled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'FF81A075-09D6-4B86-97F1-F689EC7A8ACE', N'14639965-715C-4BB5-894F-FD33C26068C1', N'1', N'常规一对一       ', 0, 1, 99, NULL, NULL, CAST(N'2016-03-18 09:29:21.823' AS DateTime), NULL, NULL, CAST(N'2016-03-18 09:29:21.823' AS DateTime))

INSERT [PM].[CategoryCatalogs] ([CategoryID], [CatalogID], [Catalog], [CatalogName], [HasPartner], [Eanbled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'FF81A075-09D6-4B86-97F1-F689EC7A8ACE', N'2F37E771-01DF-413B-9BB3-94F9C4005D43', N'102', N'合作一对一', 1, 1, 99, NULL, NULL, CAST(N'2016-03-18 09:29:21.823' AS DateTime), NULL, NULL, CAST(N'2016-03-18 09:29:21.823' AS DateTime))

INSERT [PM].[CategoryCatalogs] ([CategoryID], [CatalogID], [Catalog], [CatalogName], [HasPartner], [Eanbled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'61CA053D-F715-4798-A295-418FC824D838', N'E1DD0198-1A2A-47A7-88DE-3A29D353DEEE', N'2', N'常规班组', 0, 1, 99, NULL, NULL, CAST(N'2016-03-18 09:29:21.823' AS DateTime), NULL, NULL, CAST(N'2016-03-18 09:29:21.823' AS DateTime))

INSERT [PM].[CategoryCatalogs] ([CategoryID], [CatalogID], [Catalog], [CatalogName], [HasPartner], [Eanbled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'61CA053D-F715-4798-A295-418FC824D838', N'3BC50621-FF7B-4B8F-983A-9439B37E51DE', N'202', N'合作班组', 1, 1, 99, NULL, NULL, CAST(N'2016-03-18 09:59:43.067' AS DateTime), NULL, NULL, CAST(N'2016-03-18 09:59:43.067' AS DateTime))

INSERT [PM].[CategoryCatalogs] ([CategoryID], [CatalogID], [Catalog], [CatalogName], [HasPartner], [Eanbled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'801938F4-AF2A-4612-AC39-2A0587422F2D', N'E39EC641-F168-41B2-B300-5A9C959C4FD2', N'8', N'常规国内游学', 0, 1, 99, NULL, NULL, CAST(N'2016-03-18 10:03:31.110' AS DateTime), NULL, NULL, CAST(N'2016-03-18 10:03:31.110' AS DateTime))

INSERT [PM].[CategoryCatalogs] ([CategoryID], [CatalogID], [Catalog], [CatalogName], [HasPartner], [Eanbled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'801938F4-AF2A-4612-AC39-2A0587422F2D', N'826CB61C-2492-42FA-8411-833F147B33DA', N'802', N'合作国内游学', 1, 1, 99, NULL, NULL, CAST(N'2016-03-18 10:04:01.083' AS DateTime), NULL, NULL, CAST(N'2016-03-18 10:04:01.083' AS DateTime))

INSERT [PM].[CategoryCatalogs] ([CategoryID], [CatalogID], [Catalog], [CatalogName], [HasPartner], [Eanbled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'801938F4-AF2A-4612-AC39-2A0587422F2E', N'75A1A3C5-F148-4606-A26E-1BE809208F82', N'803', N'常规国际游学', 0, 1, 99, NULL, NULL, CAST(N'2016-03-18 10:04:15.193' AS DateTime), NULL, NULL, CAST(N'2016-03-18 10:04:15.193' AS DateTime))

INSERT [PM].[CategoryCatalogs] ([CategoryID], [CatalogID], [Catalog], [CatalogName], [HasPartner], [Eanbled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'801938F4-AF2A-4612-AC39-2A0587422F2E', N'40375E6D-D3CA-4D74-A3EB-044BB8643DA7', N'804', N'合作国际游学', 1, 1, 99, NULL, NULL, CAST(N'2016-03-18 10:04:41.380' AS DateTime), NULL, NULL, CAST(N'2016-03-18 10:04:41.380' AS DateTime))

INSERT [PM].[CategoryCatalogs] ([CategoryID], [CatalogID], [Catalog], [CatalogName], [HasPartner], [Eanbled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'9FA78CA1-9D48-4DF5-AA96-93F5DD0A7D48', N'F03F5867-FDA2-43D8-8DD6-8ACFFEC4607B', N'9', N'常规实物产品', 0, 1, 99, NULL, NULL, CAST(N'2016-03-18 10:09:35.140' AS DateTime), NULL, NULL, CAST(N'2016-03-18 10:09:35.140' AS DateTime))

INSERT [PM].[CategoryCatalogs] ([CategoryID], [CatalogID], [Catalog], [CatalogName], [HasPartner], [Eanbled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'9FA78CA1-9D48-4DF5-AA96-93F5DD0A7D48', N'2534DAE2-9363-4ACF-865E-7E4F63236CA8', N'902', N'合作实物产品', 1, 1, 99, NULL, NULL, CAST(N'2016-03-18 10:10:23.373' AS DateTime), NULL, NULL, CAST(N'2016-03-18 10:10:23.373' AS DateTime))

INSERT [PM].[CategoryCatalogs] ([CategoryID], [CatalogID], [Catalog], [CatalogName], [HasPartner], [Eanbled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'41B14CE3-B3A6-48BB-B3C2-68E6CCE7679F', N'EA2E2700-56A0-4F11-B624-56C13F94644E', N'A01', N'常规虚拟产品', 0, 1, 99, NULL, NULL, CAST(N'2016-03-18 10:11:04.113' AS DateTime), NULL, NULL, CAST(N'2016-03-18 10:11:04.113' AS DateTime))

INSERT [PM].[CategoryCatalogs] ([CategoryID], [CatalogID], [Catalog], [CatalogName], [HasPartner], [Eanbled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'41B14CE3-B3A6-48BB-B3C2-68E6CCE7679F', N'5731CB0A-B45A-4264-B362-35B42FD664FB', N'A02', N'合作虚拟产品', 1, 1, 99, NULL, NULL, CAST(N'2016-03-18 10:11:22.407' AS DateTime), NULL, NULL, CAST(N'2016-03-18 10:11:22.407' AS DateTime))

INSERT [PM].[CategoryCatalogs] ([CategoryID], [CatalogID], [Catalog], [CatalogName], [HasPartner], [Eanbled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'CA8778A1-4DE2-4EDF-BC85-956C6B7464B5', N'9EF85C13-70D6-4074-9693-F128493DBC34', N'B01', N'常规费用产品', 0, 1, 99, NULL, NULL, CAST(N'2016-03-18 10:11:52.380' AS DateTime), NULL, NULL, CAST(N'2016-03-18 10:11:52.380' AS DateTime))

INSERT [PM].[CategoryCatalogs] ([CategoryID], [CatalogID], [Catalog], [CatalogName], [HasPartner], [Eanbled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'CA8778A1-4DE2-4EDF-BC85-956C6B7464B5', N'36B28B7F-6790-487B-BE3F-A48203557729', N'B02', N'合作费用产品', 1, 1, 99, NULL, NULL, CAST(N'2016-03-18 10:12:11.280' AS DateTime), NULL, NULL, CAST(N'2016-03-18 10:12:11.280' AS DateTime))

INSERT [PM].[CategoryCatalogs] ([CategoryID], [CatalogID], [Catalog], [CatalogName], [HasPartner], [Eanbled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'49FCC428-9CB1-432A-9535-BCEC5CB5B0AB', N'126DE165-CA27-4E75-B18F-BFC841C3C321', N'C01', N'常规其它产品', 0, 1, 99, NULL, NULL, CAST(N'2016-03-18 10:12:35.697' AS DateTime), NULL, NULL, CAST(N'2016-03-18 10:12:35.697' AS DateTime))

INSERT [PM].[CategoryCatalogs] ([CategoryID], [CatalogID], [Catalog], [CatalogName], [HasPartner], [Eanbled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'49FCC428-9CB1-432A-9535-BCEC5CB5B0AB', N'BDF09909-2EB6-44A5-A65C-CDF287295ACA', N'C02', N'合作其它产品', 1, 1, 99, NULL, NULL, CAST(N'2016-03-18 10:12:52.307' AS DateTime), NULL, NULL, CAST(N'2016-03-18 10:12:52.307' AS DateTime))

INSERT [PM].[CategoryCatalogs] ([CategoryID], [CatalogID], [Catalog], [CatalogName], [HasPartner], [Eanbled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'90C34E75-E2A7-4005-8716-3EB966F2E44B', N'EF6B377B-2101-4A09-8D9B-6FCF8B164C94', N'D01', N'无课收合作产品', 1, 1, 99, NULL, NULL, CAST(N'2016-03-18 10:13:50.127' AS DateTime), NULL, NULL, CAST(N'2016-03-18 10:13:50.127' AS DateTime))

INSERT [PM].[CategoryCatalogs] ([CategoryID], [CatalogID], [Catalog], [CatalogName], [HasPartner], [Eanbled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'49FCC428-9CB1-432A-9535-BCEC5CB5B0AB', N'A2B9DBB9-3933-4695-A5EE-DCF84740B1BB', N'C03', N'常规留学', 0, 1, 99, NULL, NULL, CAST(N'2016-04-05 11:04:00.040' AS DateTime), NULL, NULL, CAST(N'2016-04-05 11:04:00.040' AS DateTime))

INSERT [PM].[CategoryCatalogs] ([CategoryID], [CatalogID], [Catalog], [CatalogName], [HasPartner], [Eanbled], [SortNo], [CreatorID], [CreatorName], [CreateTime], [ModifierID], [ModifierName], [ModifyTime]) VALUES (N'49FCC428-9CB1-432A-9535-BCEC5CB5B0AB', N'46291564-0A9C-4C6C-8C06-9144F890CD8D', N'C04', N'合作留学', 1, 1, 99, NULL, NULL, CAST(N'2016-04-05 11:04:29.140' AS DateTime), NULL, NULL, CAST(N'2016-04-05 11:04:29.140' AS DateTime))

END