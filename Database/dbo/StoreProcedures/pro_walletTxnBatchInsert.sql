DROP PROCEDURE IF EXISTS [dbo].[pro_walletTxnBatchInsert]
GO

CREATE PROCEDURE [dbo].[pro_walletTxnBatchInsert] @txns type_wallet_txn READONLY
AS
INSERT INTO t_wallet_txn (f_walletId, f_amount, f_balance, f_createdAt)
OUTPUT inserted.*
SELECT f_walletId, f_amount, f_balance, f_createdAt
FROM @txns
    RETURN 0
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[pro_walletTxnBatchInsert] TO PUBLIC
    AS [dbo];
