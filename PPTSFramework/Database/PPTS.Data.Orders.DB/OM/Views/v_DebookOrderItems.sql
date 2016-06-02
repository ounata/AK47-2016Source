
CREATE VIEW [OM].[v_DebookOrderItems]
AS
SELECT  OM.DebookOrders.CampusID, OM.DebookOrders.CampusName, OM.DebookOrders.ParentID, 
                   OM.DebookOrders.ParentName, OM.DebookOrders.CustomerID, OM.DebookOrders.CustomerCode, 
                   OM.DebookOrders.CustomerName, OM.DebookOrders.DebookID, OM.DebookOrders.DebookNo, 
                   OM.DebookOrders.DebookTime, OM.DebookOrders.DebookStatus, OM.DebookOrders.DebookMemo, 
                   OM.DebookOrders.ProcessStatus, OM.DebookOrders.ProcessTime, OM.DebookOrders.ProcessMemo, 
                   OM.DebookOrders.ContactTel, OM.DebookOrders.Contacter, OM.DebookOrders.SubmitterID, 
                   OM.DebookOrders.SubmitterName, OM.DebookOrders.SubmitterJobID, OM.DebookOrders.SubmitterJobName, 
                   OM.DebookOrders.SubmitTime, OM.DebookOrders.CreatorID, OM.DebookOrders.CreatorName, 
                   OM.DebookOrders.CreateTime, OM.DebookOrders.ModifierID, OM.DebookOrders.ModifierName, 
                   OM.DebookOrders.ModifyTime, OM.DebookOrderItems.SortNo, OM.DebookOrderItems.ItemID, 
                   OM.DebookOrderItems.AssetID, OM.v_OrderItems.AssetCode, OM.DebookOrderItems.AccountID, 
                   OM.DebookOrderItems.AccountCode, OM.DebookOrderItems.DebookAmount, OM.DebookOrderItems.DebookMoney, 
                   OM.DebookOrderItems.PresentAmountOfDebook, OM.DebookOrderItems.ReturnMoney, OM.v_OrderItems.OrderID, 
                   OM.v_OrderItems.OrderNo, OM.v_OrderItems.OrderTime, OM.v_OrderItems.OrderKind, OM.v_OrderItems.OrderType, 
                   OM.v_OrderItems.ItemID AS OrderItemID, OM.v_OrderItems.ItemNo AS OrderItemNo, OM.v_OrderItems.ProductID, 
                   OM.v_OrderItems.ProductCode, OM.v_OrderItems.ProductName, OM.v_OrderItems.OrderPrice, 
                   OM.v_OrderItems.OrderAmount, OM.v_OrderItems.PresentAmount, OM.v_OrderItems.RealPrice, 
                   OM.v_OrderItems.RealAmount, OM.v_OrderItems.ConfirmedAmount, OM.v_OrderItems.ConfirmedMoney
FROM      OM.DebookOrders INNER JOIN
                   OM.DebookOrderItems ON OM.DebookOrders.DebookID = OM.DebookOrderItems.DebookID INNER JOIN
                   OM.v_OrderItems ON OM.DebookOrderItems.AssetID = OM.v_OrderItems.AssetID

GO