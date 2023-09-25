using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace _26_TranGiaBao_Demo40
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private readonly HttpClient client = new HttpClient
        {
            MaxResponseContentBufferSize = 1_000_000
        };
        private readonly IEnumerable<string> UrList = new string[]
        {
         "https://docs.microsoft.com",
"https://docs.microsoft.com/azure",
"https://docs.microsoft.com/powershell",
"https://docs.microsoft.com/dotnet",
"https://docs.microsoft.com/aspnet/core",
"https://docs.microsoft.com/windows"
        };
        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;
            txtResults.Clear();
            await SumPageSizesAsync();
            txtResults.Text += "\nControl returned to " + nameof(btnStart_Click);
        }

        private async Task SumPageSizesAsync()
        {
            var stopwatch = Stopwatch.StartNew();
            int total = 0;
            foreach (string s in UrList)
            {
                int contentLength = await ProcessUrlAsync(s, client);
                total += contentLength;
            }
            stopwatch.Stop();
            txtResults.Text += $"\nTotal bytes returned: {total:#,#}";
            txtResults.Text += $"\nElaped time :         {stopwatch.Elapsed}\n";
        }

        private async Task<int> ProcessUrlAsync(string s, HttpClient client)
        {
            byte[] content = await client.GetByteArrayAsync(s);
            DisplayResults(s, content);
            return content.Length;
        }

        private void DisplayResults(string s, byte[] content)
        {
            txtResults.Text += $"{UrList,-60} {content.Length,10:#,#}\n";
        }
        protected override void OnClosed(EventArgs e)
        {
            client.Dispose();
        }
    }
}
