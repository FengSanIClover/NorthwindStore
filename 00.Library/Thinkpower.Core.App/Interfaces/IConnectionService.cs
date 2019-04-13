using System;
using System.Collections.Generic;
using System.Text;

namespace Thinkpower.Core.App.Interfaces
{
    public interface IConnectionService
    {
        bool IsConnected { get; }
    }
}
