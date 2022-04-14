using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using WalletManager.Domain.Model.Po;
using WalletManager.Domain.Model.Wallet;
using WalletManager.Domain.Repository;

namespace WalletManager.Persistent.Repository
{
    public class WalletRepository : ISqlRepository, IWalletRepository
    {

        public WalletRepository(string connStr): base(connStr)
        {
        }

        public (Exception exception, WalletPo walletPo) Insert(WalletPo wallet)
        {
            try
            {
                using (var cn = new SqlConnection(connStr))
                {
                    var result = cn.QueryFirstOrDefault<WalletPo>(
                        "pro_walletInsert",
                        new {wallet.f_balance},
                        commandType: CommandType.StoredProcedure);
                    return (null, result);
                }
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }

        public (Exception exception, WalletPo walletPo, TxnStatus opStatus) AddBalance(int walletId,
            decimal amount)
        {
            try
            {
                using (var cn = new SqlConnection(connStr))
                {
                    var result = cn.QueryMultiple(
                        "pro_walletAddBalance",
                        new
                        {
                            id = walletId, amount
                        },
                        commandType: CommandType.StoredProcedure);
                    var opStatus = (TxnStatus) result.ReadFirstOrDefault<int>();
                    var walletPo = result.ReadFirstOrDefault<WalletPo>();
                    return (null, walletPo, opStatus);
                }
            }
            catch (Exception ex)
            {
                return (ex, null, TxnStatus.UnknownError);
            }
        }

        public (Exception exception, IEnumerable<WalletPo> walletPos) Query(int? walletId)
        {
            try
            {
                using (var cn = new SqlConnection(connStr))
                {
                    var result = cn.Query<WalletPo>(
                        "pro_walletQuery",
                        new
                        {
                            id = walletId
                        },
                        commandType: CommandType.StoredProcedure);
                    return (null, result);
                }
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }
    }
}