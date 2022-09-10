CREATE TABLE CartsProducts(
 CartsProductsID UNIQUEIDENTIFIER PRIMARY KEY, 
 FK_ProductID UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Users(ProductID),
 FK_CartID UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Users(CartID))

