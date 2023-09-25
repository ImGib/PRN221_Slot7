internal class Program
{
    public static async Task<int> Method1()
    {
        int count = 0;
        await Task.Run(() =>
        {
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine("method 1");
                count++;
            }
        });
        return count;
    }
    public static void Method2()
    {
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine("method 2");

        }
    }
    public static void Method3(int count)
    {

        Console.WriteLine("method 3 is called");
        Console.WriteLine("Total count is " + count);

    }
    public static async Task callmethod()
    {
        Method2();
        var count = await Method1();
        Method3(count);
    }
    static async Task Main(string[] args)
    {
        await callmethod();
        Console.ReadKey();
    }
}