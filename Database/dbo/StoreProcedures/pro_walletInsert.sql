DROP PROCEDURE IF EXISTS [dbo].[pro_walletInsert]
GO

CREATE PROCEDURE [dbo].[pro_walletInsert] @f_balance decimal = 0
AS
INSERT INTO t_wallet(f_balance)
OUTPUT inserted.*
VALUES (@f_balance)
    RETURN 0
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[pro_walletInsert] TO PUBLIC
    AS [dbo];
