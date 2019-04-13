using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace Thinkpower.Core.App.Helpers
{
    public static class DeviceHelper
    {
        public static void OpenUri(Uri uri)
        {
#if DEBUG
            Debug.WriteLine(uri.ToString());
#else
            Device.OpenUri(uri);
#endif
        }

        public static void OpenGoogleMap(string addr)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    addr = string.Format("http://maps.google.com/?q={0}", System.Net.WebUtility.UrlEncode(addr));
                    break;
                case Device.Android:
                    addr = string.Format("geo:0,0?q={0}", System.Net.WebUtility.UrlEncode(addr));
                    break;
            }

            var url = new Uri(addr);
#if DEBUG
            Debug.WriteLine(url.ToString());
#else
            Device.OpenUri(url);
#endif
        }

        public static void SkypeCallByTel(string tel)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    tel = $"tel:{tel}";
                    break;
                case Device.Android:
                    break;
            }

            var uri = new System.Uri($"ms-sfb://call?id={tel}");
#if DEBUG
            Debug.WriteLine(uri.ToString());
#else
            Device.OpenUri(uri);
#endif
        }
    }
}
