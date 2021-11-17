-- T1 --

BEGIN TRAN
UPDATE Profile SET FirstName = 'NameT1'
WHERE Id = 1


UPDATE Players SET Market_Value = 10
WHERE Id = 1
