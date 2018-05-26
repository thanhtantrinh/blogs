UPDATE dbo.Product SET [Description]=N'<p>Giá mặt hàng rau củ quả không cố định và sẽ thay đổi theo mùa.</p>' WHERE CategoryId IN (38,39,40,41);

SELECT * FROM dbo.v_CategoryOfProduct WHERE CatalogueId=3 AND ParentId IN (20,18)

SELECT * FROM tant4482_db..v_Product WHERE ProductId IN (SELECT DISTINCT ProductId  FROM tant4482_db..v_Product WHERE CatalogueId=3 AND CategoryId=23)

