using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Thinkpower.Core.App.Interfaces
{
    public interface IFingerprintService
    {
        bool IsSupport { get; }

        bool IsSetup { get; }

        bool IsKeyguardSecure { get; }

        Task<FingerprintResults> Start();
    }

    public enum FingerprintResults
    {
        Error,
        Failed,
        Succeeded,
        Cancel,
        NeedPermission
    }
}
