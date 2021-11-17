
-- INSERT INTO --

BEGIN TRAN

INSERT INTO Profile ([FirstName], [LastName], [BirthDate], [Age]) VALUES ('test', 'test', '2002-06-18T10:34:09', 19)

WAITFOR DELAY '00:00:05'

ROLLBACK