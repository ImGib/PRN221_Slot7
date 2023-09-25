static void PrinNumber(string messange)
{
    for(int i =1; i<=5; i++)
    {
        Console.WriteLine(messange + ":" + i);
        Thread.Sleep(1000);
    }
}

Thread.CurrentThread.Name = "Main";
Task task1 = new Task(() => PrinNumber("Task 01"));
task1.Start();
Task task2 = Task.Run(delegate
{
    PrinNumber("Task 02");
});

Task task03 = new Task(new Action(() =>
{
    PrinNumber("Task 03");
}));
task03.Start();
Console.WriteLine($"Thread '{Thread.CurrentThread}'");
Console.ReadKey();