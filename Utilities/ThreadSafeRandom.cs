namespace RecruitmentUiTest.Utilities;

public class ThreadSafeRandom
{
    private static readonly Random _global = new Random();
    [ThreadStatic] private static Random? _local;

    public int Next(int? min = null, int? max = null)
    {
        if (_local == null)
        {
            int seed;
            lock (_global)
            {
                seed = _global.Next();
            }
            _local = new Random(seed);
        }

        return _local.Next(min ?? 0, max ?? int.MaxValue);
    }
}