CREATE TABLE [WF].[DELEGATIONS] (
    [SOURCE_USER_ID]        NVARCHAR (36) NOT NULL,
    [DESTINATION_USER_ID]   NVARCHAR (36) NOT NULL,
	[APPLICATION_NAME]		NVARCHAR (36) NOT NULL DEFAULT '',
	[PROGRAM_NAME]			NVARCHAR (36) NOT NULL DEFAULT '',
    [SOURCE_USER_NAME]      NVARCHAR (64) NULL,
    [DESTINATION_USER_NAME] NVARCHAR (64) NULL,
    [START_TIME]            DATETIME      NULL,
    [END_TIME]              DATETIME      NULL,
    [ENABLED]               NCHAR (1)     CONSTRAINT [DF_DELEGATIONS_ENABLED] DEFAULT ((1)) NULL,
	[TENANT_CODE] NVARCHAR(36) NOT NULL DEFAULT 'D5561180-7617-4B67-B68B-1F0EA604B509', 
    CONSTRAINT [PK_DELEGATIONS] PRIMARY KEY ([SOURCE_USER_ID], [DESTINATION_USER_ID], [APPLICATION_NAME], [PROGRAM_NAME], [TENANT_CODE])
);

GO

CREATE INDEX [IX_DELEGATIONS_TENANT_CODE] ON [WF].[DELEGATIONS] ([TENANT_CODE])

GO
CREATE NONCLUSTERED INDEX [IX_DELEGATIONS_DESTINATION]
    ON [WF].[DELEGATIONS]([DESTINATION_USER_ID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户定义的委托待办信息', @level0type = N'SCHEMA', @level0name = N'WF', @level1type = N'TABLE', @level1name = N'DELEGATIONS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'委托人的用户ID', @level0type = N'SCHEMA', @level0name = N'WF', @level1type = N'TABLE', @level1name = N'DELEGATIONS', @level2type = N'COLUMN', @level2name = N'SOURCE_USER_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'被委托人的用户ID', @level0type = N'SCHEMA', @level0name = N'WF', @level1type = N'TABLE', @level1name = N'DELEGATIONS', @level2type = N'COLUMN', @level2name = N'DESTINATION_USER_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'委托人的用户名称', @level0type = N'SCHEMA', @level0name = N'WF', @level1type = N'TABLE', @level1name = N'DELEGATIONS', @level2type = N'COLUMN', @level2name = N'SOURCE_USER_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'被委托人的用户名称', @level0type = N'SCHEMA', @level0name = N'WF', @level1type = N'TABLE', @level1name = N'DELEGATIONS', @level2type = N'COLUMN', @level2name = N'DESTINATION_USER_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'开始时间(UTC)', @level0type = N'SCHEMA', @level0name = N'WF', @level1type = N'TABLE', @level1name = N'DELEGATIONS', @level2type = N'COLUMN', @level2name = N'START_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'结束时间(UTC)', @level0type = N'SCHEMA', @level0name = N'WF', @level1type = N'TABLE', @level1name = N'DELEGATIONS', @level2type = N'COLUMN', @level2name = N'END_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否启用', @level0type = N'SCHEMA', @level0name = N'WF', @level1type = N'TABLE', @level1name = N'DELEGATIONS', @level2type = N'COLUMN', @level2name = N'ENABLED';


GO

CREATE INDEX [IX_DELEGATIONS_SOURCE_USER_NAME] ON [WF].[DELEGATIONS] ([SOURCE_USER_NAME])

GO

CREATE INDEX [IX_DELEGATIONS_DESTINATION_USER_NAME] ON [WF].[DELEGATIONS] ([DESTINATION_USER_NAME])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'应用名称',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'DELEGATIONS',
    @level2type = N'COLUMN',
    @level2name = N'APPLICATION_NAME'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'应用模块名称',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'DELEGATIONS',
    @level2type = N'COLUMN',
    @level2name = N'PROGRAM_NAME'