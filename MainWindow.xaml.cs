using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.ComponentModel;
using System.Net;
using System.Windows;

namespace onevpnBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Uri mUrl;
        private string mCookieName;
        private CoreWebView2Cookie? mCookie = null;
        private bool mCloseAfterCookie = true;

        public MainWindow(Uri url, string cookieName, bool closeAfterCookie)
        {
            InitializeComponent();

            mUrl = url;
            mCookieName = cookieName;
            mCloseAfterCookie = closeAfterCookie;

            this.webView.NavigationCompleted += CheckCookies;
            this.webView.Source = mUrl;
            this.Closing += PrintCookie;

        }

        private void PrintCookie(object? sender, CancelEventArgs e)
        {
            if (mCookie != null)
            {
                Console.WriteLine(mCookie.Name + "=" + mCookie.Value);
            }
        }

        private async void CheckCookies(object? sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (sender == null || sender is not WebView2)
                return;

            var webview = (WebView2)sender;

            var cookies = await webview.CoreWebView2.CookieManager.GetCookiesAsync(mUrl.ToString());

            foreach (var cookie in cookies)
            {
                if (!cookie.Name.Equals(mCookieName, StringComparison.OrdinalIgnoreCase))
                    continue;
                mCookie = cookie;

                if (mCloseAfterCookie)
                    this.Close();
            }


        }


    }
}
