CREATE PROCEDURE [CM].[p_CustomerGraduated]
AS
begin
update Customers set Graduated = 1 where (Grade = '33' or Grade = '34') and (StudentStatus = '11' or StudentStatus = '13') 
end
