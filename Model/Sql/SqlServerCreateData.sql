
USE [practicamad]


INSERT INTO UserProfile
	VALUES('admin', 'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=', 'marco', 'polo', 'marcop@udc.es', 'es', 'ES', 1, 'sitio 15A');

INSERT INTO UserProfile
	VALUES('user', 'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=', 'Ursula', 'Martinez', 'usri_mp@udc.es', 'es', 'ES', 0, 'sitio 16A');

INSERT INTO Category
	VALUES ('category1');

INSERT INTO Category
	VALUES ('category2');

INSERT INTO Category
	VALUES ('category3');

INSERT INTO Product
	VALUES ('produc1', 10.2, GETDATE(), 100, 1);

INSERT INTO Product
	VALUES ('produc2', 11.2, GETDATE(), 100, 1);

INSERT INTO Product
	VALUES ('produc3', 12.2, GETDATE(), 100, 1);

INSERT INTO Product
	VALUES ('item1', 20.6, GETDATE(), 5, 2);

INSERT INTO Product
	VALUES ('item2', 10, GETDATE(), 200, 2);

INSERT INTO Product
	VALUES ('item3', 9, GETDATE(), 10, 2);

INSERT INTO Product
	VALUES ('object1', 30, GETDATE(), 2, 3);

INSERT INTO Product
	VALUES ('object2', 10, GETDATE(), 50, 3);

INSERT INTO Product
	VALUES ('object3', 8, GETDATE(), 10, 3);

INSERT INTO Product
	VALUES ('object4', 8.7, GETDATE(), 10, 3);

INSERT INTO Tag
	VALUES ('tag1');

	INSERT INTO Tag
	VALUES ('tag2');

INSERT INTO Comment
	VALUES('comment1', GETDATE(), 2 , 1);

INSERT INTO Comment
	VALUES('comment2', GETDATE(), 2 , 2);

INSERT INTO Comment
	VALUES('comment3', GETDATE(), 2 , 3);

INSERT INTO Comment
	VALUES('comment4', GETDATE(), 2 , 4);

INSERT INTO Comment
	VALUES('comment5', GETDATE(), 2 , 6);

INSERT INTO Comment
	VALUES('comment6', GETDATE(), 2 , 7);

INSERT INTO CommentTag
	VALUES(1,1);

INSERT INTO CommentTag
	VALUES(1,2);

INSERT INTO CommentTag
	VALUES(2,1);

INSERT INTO CommentTag
	VALUES(3,1);

INSERT INTO CommentTag
	VALUES(4,2);

INSERT INTO CommentTag
	VALUES(5,2);

INSERT INTO CommentTag
	VALUES(6,2);