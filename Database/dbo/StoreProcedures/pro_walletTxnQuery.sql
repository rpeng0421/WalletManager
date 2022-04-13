DROP PROCEDURE IF EXISTS [dbo].[pro_walletTxnQuery]
GO

CREATE PROCEDURE [dbo].[pro_walletTxnQuery] @id int = NULL,
                                            @walletId int = NULL
AS
SELECT f_id, f_walletId, f_amount, f_balance, f_createdAt
FROM [dbo].t_wallet_txn
WHERE (@id is null or f_id = @id)
  and (@walletId is null or f_walletId = @walletId)
    RETURN 0
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[pro_walletTxnQuery] TO PUBLIC
    AS [dbo];
