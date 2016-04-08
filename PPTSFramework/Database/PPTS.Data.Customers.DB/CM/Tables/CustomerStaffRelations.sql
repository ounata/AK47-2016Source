CREATE TABLE [CM].[CustomerStaffRelations]
(
	[ID] NVARCHAR(36) NOT NULL DEFAULT newid() , 
    [RelationType] NVARCHAR(32) NOT NULL,
	[CustomerID] NVARCHAR(36) NOT NULL , 
    [OrgID] NVARCHAR(36) NOT NULL, 
    [OrgName] NVARCHAR(64) NULL, 
    [StaffID] NVARCHAR(36) NOT NULL, 
    [StaffName] NVARCHAR(64) NULL, 
    [StaffJobID] NVARCHAR(36) NOT NULL, 
    [StaffJobName] NVARCHAR(64) NULL, 
	[CreatorID] NVARCHAR(36) NULL,
	[CreatorName] NVARCHAR(64) NULL,
	[CreateTime] DATETIME NOT NULL DEFAULT getdate(),
	[TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_CustomerStaffRelations] PRIMARY KEY NONCLUSTERED ([ID]), 
    CONSTRAINT [IX_CustomerStaffRelations] UNIQUE ([CustomerID], [StaffJobID])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'CustomerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'员工ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'StaffID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'员工名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'StaffName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户员工的关系表',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = NULL,
    @level2name = NULL
GO

CREATE INDEX [IX_CustomerStaffRelations_1] ON [CM].[CustomerStaffRelations] ([StaffJobID])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'1 销售关系: 学生，销售（咨询师）[原来写的是家长，销售关系，但是看到逻辑是学生和销售关系];2 教管关系：学生，学管（班主任）;3 教学关系: 学生，老师;4 电销关系',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'RelationType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'租户的ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'TenantCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'主键',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = 'ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'员工岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'StaffJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'员工岗位名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'StaffJobName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'员工所属组织机构ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'OrgID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'员工所属组织机构名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'OrgName'