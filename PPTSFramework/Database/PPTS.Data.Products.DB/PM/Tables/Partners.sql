
CREATE TABLE [PM].[Partners](
	[PartnerID] [nvarchar](36) NOT NULL,
	[PartnerName] [nvarchar](50) NULL,
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[ModifierID] [nvarchar](36) NULL,
	[ModifierName] [nvarchar](50) NULL,
	[ModifyTime] [datetime] NULL,
	[TenantCode] [nvarchar](36) NULL,
 CONSTRAINT [PK_Partners] PRIMARY KEY NONCLUSTERED 
(
	[PartnerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [PM].[Partners] ADD  CONSTRAINT [DF_Partners_PartnerID]  DEFAULT newid() FOR [PartnerID]
GO
ALTER TABLE [PM].[Partners] ADD  CONSTRAINT [DF_Partners_CreateTime]  DEFAULT GETUTCDATE() FOR [CreateTime]
GO
ALTER TABLE [PM].[Partners] ADD  CONSTRAINT [DF_Partners_ModifyTime]  DEFAULT GETUTCDATE() FOR [ModifyTime]
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'合作伙伴表',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Partners',
    @level2type = NULL,
    @level2name = NULL