CREATE PROCEDURE [CM].[p_CustomerGraduated]
AS
begin
update CM.Customers set Graduated = 1,GraduateYear=GETUTCDATE() where (Grade = '33' or Grade = '34') and (StudentStatus = '11' or StudentStatus = '13') 
end
