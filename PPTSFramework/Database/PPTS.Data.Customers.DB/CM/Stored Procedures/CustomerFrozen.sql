CREATE PROCEDURE [CM].[p_CustomerFrozen]
AS
begin
update Customers set StudentStatus = '14' where Graduated = 1 and StudentStatus != '14'
end
