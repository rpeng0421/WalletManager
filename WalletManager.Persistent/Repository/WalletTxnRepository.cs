using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using WalletManager.Domain.Model.Po;
using WalletManager.Domain.Repository;

namespace WalletManager.Persistent.Repository
{
    public class WalletTxnRepository : IWalletTxnRepository
    {
        private string connStr;

        public WalletTxnRepository(string connStr)
        {
            this.connStr = connStr;
        }

        public (Exception exception, IEnumerable<WalletTxnPo> walletTxnPos) Insert(IEnumerable<WalletTxnPo> walletTxnPos)
        {
            try
            {
                var udt = new DataTable();
                udt.Columns.Add(nameof(WalletTxnPo.f_id), typeof(int));
                udt.Columns.Add(nameof(WalletTxnPo.f_walletId), typeof(int));
                udt.Columns.Add(nameof(WalletTxnPo.f_amount), typeof(decimal));
                udt.Columns.Add(nameof(WalletTxnPo.f_balance), typeof(decimal));
                udt.Columns.Add(nameof(WalletTxnPo.f_createdAt), typeof(DateTime));
                foreach (var walletTxnPo in walletTxnPos)
                {
                    var dr = udt.NewRow();
                    dr[nameof(WalletTxnPo.f_id)] = walletTxnPo.f_id;
                    dr[nameof(WalletTxnPo.f_walletId)] = walletTxnPo.f_walletId;
                    dr[nameof(WalletTxnPo.f_amount)] = walletTxnPo.f_amount;
                    dr[nameof(WalletTxnPo.f_balance)] = walletTxnPo.f_balance;
                    dr[nameof(WalletTxnPo.f_createdAt)] = walletTxnPo.f_createdAt;

                    udt.Rows.Add(dr);
                }

                using (var cn = new SqlConnection(connStr))
                {
                    var result = cn.Query<WalletTxnPo>(
                        "pro_walletTxnBatchInsert",
                        new
                        {
                            txns = udt.AsTableValuedParameter("type_wallet_txn")
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

        public (Exception exception, IEnumerable<WalletTxnPo> walletTxnPos) Query(int? walletId)
        {
            try
            {
                using (var cn = new SqlConnection(connStr))
                {
                    var result = cn.Query<WalletTxnPo>(
                        "pro_walletTxnQuery",
                        new
                        {
                            walletId = walletId,
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