
USE [practicamad]


INSERT INTO UserProfile
	VALUES('admin', 'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=', 'marco', 'polo', 'marcop@udc.es', 'es', 'ES', 1, 'sitio 15A');

INSERT INTO Category
	VALUES ('category1');

INSERT INTO Category
	VALUES ('category2');

INSERT INTO Product
	VALUES ('produc1', 10.2, GETDATE(), 100, 1);

INSERT INTO Product
	VALUES ('produc2', 11.2, GETDATE(), 5, 2);
