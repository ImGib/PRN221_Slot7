var tasks=  new List<Task<int>>();
var source = new CancellationTokenSource();
var token = source.Token;
int completedIterations = 0;
for(int n=1; n<=20; n++)
{
    tasks.Add(Task.Run(() =>
    {
        int iteration = 0;
        for (int ctr = 1; ctr <= 2_000_000; ctr++)
        {
            token.ThrowIfCancellationRequested();
            iteration++;
        }
        Interlocked.Increment(ref completedIterations);
        if (completedIterations >= 10) source.Cancel();
        return iteration;
    }, token));
    Console.WriteLine("Waiting for the first 10 tasks to complete...\n");
    try
    {
        Task.WaitAll(tasks.ToArray());
    }catch (AggregateException)
    {
        Console.WriteLine("Statuc of tasks:\n");
        Console.WriteLine("{0,10} {1,20} {2,14:N0}", "Task Id", "Status", "Iterations");
        foreach (var task in tasks) Console.WriteLine("{0,10} {1,20} {2,14}",
            task.Id, task.Status, task.Status != TaskStatus.Canceled ?
            task.Result.ToString("N0") : "n/a");
    }
    Console.ReadLine();
}