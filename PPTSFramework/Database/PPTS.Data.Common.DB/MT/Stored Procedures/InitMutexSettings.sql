CREATE PROCEDURE [MT].[InitMutexSettings]
AS
BEGIN

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (0, N'充值', 0, N'充值')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (0, N'充值', 1, N'退费')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (0, N'充值', 3, N'转出')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (0, N'充值', 4, N'转学')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (0, N'充值', 6, N'退订')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (1, N'退费', 0, N'充值')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (1, N'退费', 1, N'退费')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (1, N'退费', 2, N'转入')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (1, N'退费', 3, N'转出')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (1, N'退费', 4, N'转学')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (1, N'退费', 5, N'订购')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (1, N'退费', 6, N'退订')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (2, N'转入', 1, N'退费')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (2, N'转入', 4, N'转学')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (3, N'转出', 1, N'退费')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (3, N'转出', 3, N'转出')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (3, N'转出', 4, N'转学')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (3, N'转出', 5, N'订购')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (4, N'转学', 0, N'充值')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (4, N'转学', 1, N'退费')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (4, N'转学', 2, N'转入')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (4, N'转学', 3, N'转出')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (4, N'转学', 4, N'转学')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (4, N'转学', 5, N'订购')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (4, N'转学', 6, N'退订')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (5, N'订购', 1, N'退费')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (5, N'订购', 3, N'转出')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (5, N'订购', 4, N'转学')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (6, N'退订', 1, N'退费')

INSERT [MT].[MutexSettings] ([BizAction], [BizActionText], [MutexAction], [MutexActionText]) VALUES (6, N'退订', 4, N'转学')

END