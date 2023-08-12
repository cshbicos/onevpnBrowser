using System;

namespace onevpnBrowser
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                if (args.Length != 2)
                {
                    Console.WriteLine("Call with URL and cookie name");
                    return;
                }


                var url = new Uri(args[0]);

                if (string.IsNullOrWhiteSpace(args[1]))
                {
                    Console.WriteLine("No cookie name given");
                    return;
                }

                RunApplication(url, args[1]);
            }
            catch (UriFormatException ex)
            {
                Console.WriteLine("URL " + args[0] + " incorrect");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("No URL given");
            }
        }

        private static void RunApplication(Uri url, string cookieName)
        {
            MainWindow wnd = new MainWindow(url, cookieName);
            var application = new System.Windows.Application();
            application.Run(wnd);
        }
    }
}