using System;
using System.Threading.Tasks;

namespace Thinkpower.Core.App.Interfaces
{
    public interface IDeviceService
    {
        Task<string> GetDeviceID();
    }
}
