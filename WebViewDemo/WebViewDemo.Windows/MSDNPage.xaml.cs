using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WebViewDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MSDNPage : Page
    {
        private List<Uri> historyStack;
        private int stackIndex;
        private bool fromHistory;

        public MSDNPage()
        {
            this.InitializeComponent();

            webView.NavigationCompleted += webView_NavigationCompleted;

            historyStack = new List<Uri>();
            stackIndex = 0;
            fromHistory = false;
        }
       
        void webView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (!fromHistory)
            {
                if (stackIndex < historyStack.Count)
                {
                    historyStack.RemoveRange(stackIndex, historyStack.Count - stackIndex);
                }
                historyStack.Add(args.Uri);
                stackIndex += 1;
            }
            fromHistory = false;
            pageTitle.Text = webView.DocumentTitle;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (stackIndex > 1)
            {
                stackIndex -= 1;
                fromHistory = true;
                webView.Navigate(historyStack[stackIndex - 1]);
            }
            else
            {
                if (this.Frame.CanGoBack)
                    this.Frame.GoBack();
            }
        }
    }
}
