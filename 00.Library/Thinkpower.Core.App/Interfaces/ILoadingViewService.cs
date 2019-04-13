using System;
using System.Collections.Generic;
using System.Text;

namespace Thinkpower.Core.App.Interfaces
{
    public interface ILoadingViewService
    {
        void Show();

        void Dismiss();

        bool IsShow();
    }
}
