using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using WalletManager.Domain.Model.Po;
using WalletManager.Domain.Repository;

namespace WalletManager.Persistent.Repository
{
    public class WalletRepository: IWalletRepository
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
                        "pro_userRelationUpdate",
                        wallet,
                        commandType: CommandType.StoredProcedure);
                    return (null, result);
                }
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }

        public (Exception exception, WalletPo walletPo) AddBalance(decimal amount)
        {
            throw new NotImplementedException();
        }

        public (Exception exception, WalletPo walletPo) Query(int? walletId)
        {
            throw new NotImplementedException();
        }
    }
}