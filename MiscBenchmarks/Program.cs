using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Immutable;

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<Foobar>();
    }
}

[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class Foobar
{
    static readonly int[] Enumerable = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }; 

    [Benchmark]
    public List<int> ToList()
    {
        return Enumerable.ToList();
    }

    [Benchmark]
    public int[] ToArray()
    {
        return Enumerable.ToArray();
    }

    [Benchmark]
    public ImmutableArray<int> ToImmutableArray()
    {
        return Enumerable.ToImmutableArray();
    }

    [Benchmark]
    public ImmutableList<int> ToImmutableList()
    {
        return Enumerable.ToImmutableList();
    }
}