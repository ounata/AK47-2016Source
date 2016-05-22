
CREATE VIEW [MT].[v_TeacherJobs]
AS
SELECT  MT.TeacherJobs.CampusID, MT.TeacherJobs.CampusName, MT.Teachers.TeacherID, MT.Teachers.TeacherCode, 
                   MT.Teachers.TeacherName, MT.Teachers.TeacherOACode, MT.Teachers.Gender, MT.Teachers.Birthday, 
                   MT.Teachers.GradeMemo, MT.Teachers.SubjectMemo, MT.TeacherJobs.JobID, MT.TeacherJobs.JobName, 
                   MT.TeacherJobs.JobOrgID, MT.TeacherJobs.JobOrgName,MT.TeacherJobs.JobOrgShortName, MT.TeacherJobs.JobOrgType, MT.TeacherJobs.JobStatus, 
                   MT.TeacherJobs.IsFullTime
FROM      MT.Teachers INNER JOIN
                   MT.TeacherJobs ON MT.Teachers.TeacherID = MT.TeacherJobs.TeacherID

GO