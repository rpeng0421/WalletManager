DROP PROCEDURE IF EXISTS [dbo].[pro_walletQuery]
GO

CREATE PROCEDURE [dbo].[pro_walletQuery] @id int = NULL
AS
SELECT f_id, f_balance
FROM [dbo].t_wallet
WHERE (@id is null or f_id = @id)
    RETURN 0
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[pro_walletQuery] TO PUBLIC
    AS [dbo];
