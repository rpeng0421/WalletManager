DROP PROCEDURE IF EXISTS [dbo].[pro_walletInsert]
GO

CREATE PROCEDURE [dbo].[pro_walletInsert]
	
AS
	INSERT INTO t_wallet(f_balance)
	OUTPUT inserted.*
	VALUES (0)
RETURN 0
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[pro_walletInsert] TO PUBLIC
    AS [dbo];
