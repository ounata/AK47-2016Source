CREATE PROCEDURE [CM].[p_CustomerAutoUpgrade]
AS
begin

update 
   Customers 
set 
   Grade = (select UpgradedGrade from CustomerUpgradeRelations a where a.UpgradedGrade = Customers.Grade and a.UpgradeType = 1) 
where  
   CampusID in (select CampusID from SpecialCampuses)

update 
   Customers 
set 
   Grade = (select UpgradedGrade from CustomerUpgradeRelations a where a.UpgradedGrade = Customers.Grade and a.UpgradeType = 0) 
where  
   CampusID not in (select CampusID from SpecialCampuses)

end
