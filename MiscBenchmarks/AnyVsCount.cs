using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class AnyVsCount
{
    const int nrOfElements = 1000;
    static readonly int[] Input = new int[nrOfElements];

    static AnyVsCount()
    {
        var rng = new Random(666);
        for (int i = 0; i < nrOfElements; i++)
        {
            Input[i] = rng.Next(2048);
        }
    }

    [Benchmark]
    public bool Any()
    {
        return Input.Any(x => x < 1234);
    }

    [Benchmark]
    public bool CountEqZero()
    {
        return Input.Count(x => x < 1234) > 0;
    }
}
