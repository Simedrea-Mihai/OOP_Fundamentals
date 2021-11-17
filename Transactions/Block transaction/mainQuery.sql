
BEGIN TRAN

SELECT * FROM Profile WHERE age = 36

INSERT INTO Profile ([FirstName], [LastName], [BirthDate], [Age]) VALUES ('test', 'test', '2002-06-18T10:34:09', 19)

SELECT * FROM Profile WHERE age = 36

ROLLBACK