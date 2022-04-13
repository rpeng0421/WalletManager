using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using WalletManager.Domain.Model.Po;
using WalletManager.Domain.Repository;

namespace WalletManager.Persistent.Repository
{
    public class WalletRepository : IWalletRepository
    {
        private string connStr;

        public WalletRepository(string connStr)
        {
            this.connStr = connStr;
        }

        public (Exception exception, WalletPo walletPo) Insert(WalletPo wallet)
        {
            try
            {
                using (var cn = new SqlConnection(connStr))
                {
                    var result = cn.QueryFirstOrDefault<WalletPo>(
                        "pro_walletInsert",
                        new {f_balance = wallet.f_balance},
                        commandType: CommandType.StoredProcedure);
                    return (null, result);
                }
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }

        public (Exception exception, WalletPo walletPo) AddBalance(int walletId, decimal amount)
        {
            try
            {
                using (var cn = new SqlConnection(connStr))
                {
                    var result = cn.QueryFirstOrDefault<WalletPo>(
                        "pro_walletAddBalance",
                        new
                        {
                            id = walletId,
                            amount = amount
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
                            id = walletId,
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