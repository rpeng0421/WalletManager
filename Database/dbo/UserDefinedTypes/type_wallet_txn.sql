DROP TYPE IF EXISTS [dbo].[pro_walletTxnBatchInsert]
GO

CREATE TYPE [dbo].[type_wallet_txn] AS TABLE
(
    [f_id]        INT            NOT NULL,
    [f_walletId]  INT            NOT NULL,
    [f_amount]    DECIMAL(14, 4) NOT NULL,
    [f_balance]   DECIMAL(14, 4) NOT NULL,
    [f_createdAt] DATETIME       NOT NULL
);
GO

GRANT EXECUTE
    ON TYPE::[dbo].[type_wallet_txn] TO PUBLIC;
GO