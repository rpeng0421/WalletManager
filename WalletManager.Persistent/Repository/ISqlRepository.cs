namespace WalletManager.Persistent.Repository
{
    public abstract class ISqlRepository
    {
        protected string connStr;

        protected ISqlRepository(string connStr)
        {
            this.connStr = connStr;
        }
    }
}