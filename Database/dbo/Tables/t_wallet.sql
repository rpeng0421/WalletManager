DROP TABLE IF EXISTS [dbo].[t_wallet];
CREATE TABLE [dbo].[t_wallet]
(
    [f_id]      INT IDENTITY (1, 1) NOT NULL,
    [f_balance] DECIMAL(14, 4)      NOT NULL DEFAULT 0,
    CONSTRAINT [PK_wallet] PRIMARY KEY CLUSTERED ([f_id] ASC)
)