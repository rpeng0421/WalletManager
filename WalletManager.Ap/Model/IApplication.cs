using NLog;

namespace WalletManager.Ap.Model
{
    public abstract class IApplication
    {
        public ILogger Logger { get; set; }
    }
}