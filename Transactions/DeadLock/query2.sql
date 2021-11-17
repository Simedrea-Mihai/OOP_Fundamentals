-- T2 --

BEGIN TRAN

UPDATE Players SET Market_Value = 10
WHERE Id = 1

UPDATE Profile SET FirstName = 'NameT1'
WHERE Id = 1
