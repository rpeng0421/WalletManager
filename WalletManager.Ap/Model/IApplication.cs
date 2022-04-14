using System;
using NLog;

namespace WalletManager.Ap.Model
{
    public abstract class IApplication
    {
        protected ILogger Logger { get; set; }
    }
}