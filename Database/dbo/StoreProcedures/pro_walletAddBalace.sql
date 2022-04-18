DROP PROCEDURE IF EXISTS [dbo].[pro_walletAddBalance]
GO

CREATE PROCEDURE [dbo].[pro_walletAddBalance] @id int,
                                              @amount decimal = 0
AS
DECLARE
    @sStatus     int = 0,
    @sPreBalance decimal

SELECT @sPreBalance = f_balance
FROM t_wallet WITH (NOLOCK)
WHERE f_id = @id;
    IF @sPreBalance + @amount < 0
        BEGIN
            SELECT 2
            SELECT *
            FROM t_wallet WITH (ROWLOCK)
            WHERE f_id = @id;
        END
    ELSE
        BEGIN
            SELECT 1
            UPDATE t_wallet WITH (ROWLOCK)
            SET f_balance += @amount
            OUTPUT inserted.*
            WHERE f_id = @id
        END
    RETURN 0
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[pro_walletAddBalance] TO PUBLIC
    AS [dbo];
