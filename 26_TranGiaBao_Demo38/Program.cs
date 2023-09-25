using System.Net;

internal class Program
{
    private static void DownloadAsynchronously()
    {
        WebClient client = new WebClient();
        client.DownloadDataCompleted += new DownloadDataCompletedEventHandler(DownloadComplete);
        client.DownloadStringAsync(new Uri("http://www.aspnet.com"));
    }

    private static void DownloadComplete(object sender, DownloadDataCompletedEventArgs e)
    {
        if(e.Error != null)
        {
            Console.WriteLine("Some error has oocured.");
            return;
        }
        Console.WriteLine(e.Result);
        Console.WriteLine(new string('*', 30));
        Console.WriteLine("Download completed.");
    }
    private static void Main(string[] args)
    {
        DownloadAsynchronously();
        Console.WriteLine("Main thread : Done");
        Console.WriteLine(new string('*', 30));
        Console.ReadLine();
    }
}