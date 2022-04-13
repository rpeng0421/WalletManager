DROP TABLE IF EXISTS [dbo].[t_wallet_txn];
CREATE TABLE [dbo].[t_wallet_txn]
(
    [f_id]           INT IDENTITY (1,1) NOT NULL,
    [f_walletId]     INT                NOT NULL,
    [f_preBalance]   DECIMAL(14, 4)     NOT NULL,
    [f_changeAmount] DECIMAL(14, 4)     NOT NULL,
    [f_afterBalance] DECIMAL(14, 4)     NOT NULL,
    [f_createdAt]    DATETIME           NOT NULL,
    CONSTRAINT [PK_wallet_txn] PRIMARY KEY CLUSTERED ([f_id] ASC)
)

CREATE NONCLUSTERED INDEX [IX_t_walletId] on [dbo].[t_wallet_txn] ([f_walletId] ASC)
CREATE NONCLUSTERED INDEX [IX_t_createdAt] on [dbo].[t_wallet_txn] ([f_createdAt] DESC )
go