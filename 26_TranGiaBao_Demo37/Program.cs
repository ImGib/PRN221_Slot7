using System.Collections.Concurrent;
using System.Diagnostics;

internal class Program
{
    private static bool IsPrime(int number)
    {
        bool result = true;
        if(number < 2)
        {
            result = false;
        }
        for(var div = 2; div <= Math.Sqrt(number) && result == true; div++)
        {
            if(number % div == 0)
            {
                result = false;
            }
        }
        return result;
    }
    private static IList<int> GetPrime(IList<int> numbers) => numbers.Where(IsPrime).ToList();
    private static IList<int> GetPrimeListWithParallel(IList<int> numbers)
    {
        var primeNumbers = new ConcurrentBag<int>();
        Parallel.ForEach(numbers, number =>
        {
            if (IsPrime(number))
            {
                primeNumbers.Add(number);
            }
        });
        return primeNumbers.ToList();
    }
    private static void Main(string[] args)
    {
        var limit = 2_000_000;
        var numbers = Enumerable.Range(0, limit).ToList();
        var watch = Stopwatch.StartNew();
        var primeNumbersFromForeach = GetPrime(numbers);
        watch.Stop();
        
        var watchForParallel = Stopwatch.StartNew();
        var primeNumberFromParallelForeach = GetPrimeListWithParallel(numbers);
        watchForParallel.Stop();

        Console.WriteLine($"Classical foreach loop | Total prim numbers :" +
            primeNumbersFromForeach.Count + " | Time Taken : " +
            watch.ElapsedMilliseconds + " ms.");
        Console.WriteLine($"Classical foreach loop | Total prim numbers :" +
            primeNumberFromParallelForeach.Count + " | Time taken : " +
            watchForParallel.ElapsedMilliseconds + " ms."); ;

        Console.WriteLine("Press any key to exit.");
        Console.ReadLine();
    }
}