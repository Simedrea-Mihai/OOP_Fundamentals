
/* Explicit transaction */

/* We have to specify the beginning and the ending of the transaction */

BEGIN TRAN

SELECT *
FROM Players


INSERT INTO Profile ([FirstName], [LastName], [BirthDate], [Age])
VALUES ('Test', 'Test', '2002-04-21T00:00:00', 19)

INSERT INTO Players ([Market_Value], [FreeAgent], [TeamIdPlayer])
VALUES (200000, 0, 0)

UPDATE Players
SET Market_Value = Market_Value * 1.5
WHERE Players.Id % 2 = 0

SELECT *
FROM Players, Profile

ROLLBACK TRAN