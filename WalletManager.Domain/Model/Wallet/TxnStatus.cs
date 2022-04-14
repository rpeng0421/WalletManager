namespace WalletManager.Domain.Model.Wallet
{
    public enum TxnStatus
    {
        UnknownError = -1,
        Success = 1,
        Insufficient = 2,
        IllegalAmount = 3
    }
}