USE [FashioNEXDB]
GO


INSERT INTO [FashioNEXDB].[dbo].[PRODUCTS]
           ([ProductModel_UniqueNumber]
           ,[ProductName]
           ,[ProductImageName]
           ,[ProductImage]
           ,[Price]
           ,[IsActive]
           ,[Cr_datetime]
           ,[Mo_datetime],Quantity)
     VALUES
     ('P_TS_M_DUKE_1','Duke T-Shirt','TShirt-1001',NULL,500.00,'Y',GETDATE(),GETDATE(),20),
     ('P_TS_M_ADDI_1','Addidas T-Shirt','TShirt-1001',NULL,800.00,'Y',GETDATE(),GETDATE(),20),
       ('P_TS_M_NIKE_1','Nike T-Shirt','TShirt-1001',NULL,600.00,'Y',GETDATE(),GETDATE(),20),
         ('P_TS_M_INVI_1','Invictus T-Shirt','TShirt-1001',NULL,450.00,'Y',GETDATE(),GETDATE(),20),
           ('P_TS_M_HRX0_1','HRX T-Shirt','TShirt-1001',NULL,550.00,'Y',GETDATE(),GETDATE(),20)
		   

GO