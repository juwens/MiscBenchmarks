using BenchmarkDotNet.Running;

public class Program
{
    public static void Main(string[] args)
    {
        new AnyVsCount().Any();
        new AnyVsCount().CountEqZero();


        BenchmarkRunner.Run<ObjectCreation>();
    }
}
