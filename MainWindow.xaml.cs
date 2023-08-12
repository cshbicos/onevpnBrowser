using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System;
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

        public MainWindow(Uri url, string cookieName)
        {
            InitializeComponent();

            mUrl = url;
            mCookieName = cookieName;

            this.webView.NavigationCompleted += CheckCookies;
            this.webView.Source = mUrl;
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

                Console.WriteLine(cookie.Name + "=" + cookie.Value);
                this.Close();
            }


        }
    }
}
