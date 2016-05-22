CREATE VIEW [CM].[v_CustomerTeacherRelations]
	AS SELECT   CM.CustomerTeacherRelations_Current.*, CM.Customers_Current.CustomerCode, 
                CM.Customers_Current.CustomerName
FROM      CM.Customers_Current INNER JOIN
                CM.CustomerTeacherRelations_Current ON 
                CM.Customers_Current.CustomerID = CM.CustomerTeacherRelations_Current.CustomerID
