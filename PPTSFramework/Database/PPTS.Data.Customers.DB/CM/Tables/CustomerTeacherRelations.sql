CREATE TABLE [CM].[CustomerTeacherRelations]
(
	[ID] NVARCHAR(36) NOT NULL, 
    [CustomerID] NVARCHAR(36) NULL, 
    [OrgID] NVARCHAR(36) NULL, 
    [OrgName] NVARCHAR(64) NULL, 
    [TeacherID] NVARCHAR(36) NULL, 
    [TeacherCode] NVARCHAR(64) NULL, 
    [TeacherName] NVARCHAR(64) NULL, 
    [Subject] NVARCHAR(32) NULL, 
    [Grade] NVARCHAR(32) NULL, 
    [CreatorID] NVARCHAR(36) NULL, 
    [CreatorName] NVARCHAR(64) NULL, 
    [CreateTime] DATETIME NULL, 
    CONSTRAINT [PK_CustomerTeacherRelations] PRIMARY KEY NONCLUSTERED ([ID]) 
)

GO

CREATE INDEX [IX_CustomerTeacherRelations_1] ON [CM].[CustomerTeacherRelations] ([CustomerID])

GO

CREATE INDEX [IX_CustomerTeacherRelations_2] ON [CM].[CustomerTeacherRelations] ([TeacherID])
