
USE [practicamad]


INSERT INTO UserProfile
	VALUES('admin', 'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=', 'marco', 'polo', 'marcop@udc.es', 'es', 'ES', 1, 'sitio 15A');

INSERT INTO Category
	VALUES ('category1');

INSERT INTO Product
	VALUES ('produc1', 10.2, GETDATE(), 100, 1);

INSERT INTO Delivery
	VALUES(GETDATE(), 10.2, 'Address1', 1, 1, 'Test');

INSERT INTO Delivery
	VALUES(GETDATE(), 11.2, 'Address2', 1, 1, 'Test2');

INSERT INTO Delivery
	VALUES(GETDATE(), 11.2, 'sdadasdads', 1, 1, 'adsad');

INSERT INTO DeliveryLine
	VALUES(2, 12.2, 1, 1);

INSERT INTO DeliveryLine
	VALUES(3, 10, 1, 1);

INSERT INTO DeliveryLine
	VALUES(4, 14.2, 1, 1);