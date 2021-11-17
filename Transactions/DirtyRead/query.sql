
-- UPDATE PROFILE --

BEGIN TRAN

UPDATE Profile SET FirstName = 'Test', LastName = 'Test' WHERE Id % 5 = 0
WAITFOR DELAY '00:00:10'

ROLLBACK


