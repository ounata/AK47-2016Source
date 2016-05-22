CREATE VIEW [CM].[CustomerParentPhone_Current]
AS
SELECT CM.CustomerID, CM.CustomerCode, CM.CustomerName, CM.Grade, CM.SchoolID, P.ParentID, P.ParentCode, P.ParentName, R.CustomerRole, R.ParentRole, Phone.PhoneNumber
FROM CM.Customers_Current CM LEFT JOIN CM.CustomerParentRelations_Current R ON CM.CustomerID = R.CustomerID AND R.IsPrimary = 1
	LEFT JOIN CM.Parents_Current P ON R.ParentID = P.ParentID AND R.IsPrimary = 1 LEFT JOIN 
	CM.Phones_Current Phone ON P.ParentID = Phone.OwnerID AND Phone.IsPrimary = 1
