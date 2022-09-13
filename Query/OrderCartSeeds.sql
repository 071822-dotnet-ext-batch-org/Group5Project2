DELETE FROM Carts;
DELETE FROM CartsProducts;
DELETE FROM Orders;
DELETE FROM OrdersProducts;

INSERT INTO Carts (cartTotal, cartItems, fk_userID) VALUES (0, 0, (SELECT userID FROM Users WHERE username = 'newuser'));
INSERT INTO Carts (cartTotal, cartItems, fk_userID) VALUES (299, 3, (SELECT userID FROM Users WHERE username = 'newuser2'));
INSERT INTO Carts (cartTotal, cartItems, fk_userID) VALUES (123.23, 2, (SELECT userID FROM Users WHERE username = 'newuser3'));

INSERT INTO CartsProducts (fk_productID, fk_cartID) VALUES ((SELECT productID FROM Products WHERE productName = 'Umbrella'), (SELECT cartID FROM Carts WHERE fk_userID = (SELECT userID FROM Users WHERE username = 'newuser2')));
INSERT INTO CartsProducts (fk_productID, fk_cartID) VALUES ((SELECT productID FROM Products WHERE productName = 'Jacket'), (SELECT cartID FROM Carts WHERE fk_userID  = (SELECT userID FROM Users WHERE username = 'newuser2')));
INSERT INTO CartsProducts (fk_productID, fk_cartID) VALUES ((SELECT productID FROM Products WHERE productName = 'Watch'), (SELECT cartID FROM Carts WHERE fk_userID = (SELECT userID FROM Users WHERE username = 'newuser2')));
INSERT INTO CartsProducts (fk_productID, fk_cartID) VALUES ((SELECT productID FROM Products WHERE productName = 'Jacket'), (SELECT cartID FROM Carts WHERE fk_userID = (SELECT userID FROM Users WHERE username = 'newuser3')));
INSERT INTO CartsProducts (fk_productID, fk_cartID) VALUES ((SELECT productID FROM Products WHERE productName = 'Coffee Table'), (SELECT cartID FROM Carts WHERE fk_userID = (SELECT userID FROM Users WHERE username = 'newuser3')));

INSERT INTO Orders (orderTotal, dateOrdered, fk_userID) VALUES (209.30, CURRENT_TIMESTAMP, (SELECT userID FROM Users WHERE username = 'newuser'));
INSERT INTO Orders (orderTotal, dateOrdered, dateDelivered, fk_userID) VALUES (19.12, DATEADD(month, -1, CURRENT_TIMESTAMP), DATEADD(week, -2, CURRENT_TIMESTAMP), (SELECT userID FROM Users WHERE username = 'newuser2'));
INSERT INTO Orders (orderTotal, dateOrdered, dateDelivered, cancelled, refunded, fk_userID) VALUES (4778.21, DATEADD(month, -3, CURRENT_TIMESTAMP), DATEADD(month, -2, CURRENT_TIMESTAMP), 1, 0, (SELECT userID FROM Users WHERE username = 'newuser3'));

INSERT INTO OrdersProducts (fk_productID, fk_orderID) VALUES ((SELECT productID FROM Products WHERE productName = 'Umbrella'), (SELECT orderID FROM Orders WHERE orderTotal = 209.30));
INSERT INTO OrdersProducts (fk_productID, fk_orderID) VALUES ((SELECT productID FROM Products WHERE productName = 'Jacket'), (SELECT orderID FROM Orders WHERE orderTotal = 209.30));
INSERT INTO OrdersProducts (fk_productID, fk_orderID) VALUES ((SELECT productID FROM Products WHERE productName = 'Jacket'), (SELECT orderID FROM Orders WHERE orderTotal = 19.12));
INSERT INTO OrdersProducts (fk_productID, fk_orderID) VALUES ((SELECT productID FROM Products WHERE productName = 'Coffee Table'), (SELECT orderID FROM Orders WHERE orderTotal = 4778.21));
INSERT INTO OrdersProducts (fk_productID, fk_orderID) VALUES ((SELECT productID FROM Products WHERE productName = 'Green Chair'), (SELECT orderID FROM Orders WHERE orderTotal = 4778.21));
