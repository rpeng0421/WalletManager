DROP PROCEDURE IF EXISTS [dbo].[pro_walletAddBalance]
    GO

CREATE PROCEDURE [dbo].[pro_walletAddBalance]
    @id int,
	@balance decimal = 0
AS
	UPDATE t_wallet WITH (ROWLOCK)
	SET f_balance += @balance 
	OUTPUT inserted.*
	WHERE f_id = @id
RETURN 0
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[pro_walletAddBalance] TO PUBLIC
    AS [dbo];
