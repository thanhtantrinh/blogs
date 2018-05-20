--DECLARE @sql NVARCHAR(max)=''

--SELECT @sql += ' Drop table ' + QUOTENAME(TABLE_SCHEMA) + '.'+ QUOTENAME(TABLE_NAME) + '; '
--FROM   INFORMATION_SCHEMA.TABLES
--WHERE  TABLE_TYPE = 'BASE TABLE'

--Exec Sp_executesql @sql

USE tant4482_db;
TRUNCATE TABLE dbo.Product;
TRUNCATE TABLE dbo.ProductDetail;
TRUNCATE TABLE dbo.ProductPrice;