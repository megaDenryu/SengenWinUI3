namespace SengenWinUI3.State;

// 2回 Dispose しても1回目だけが購読者を外す。
internal sealed class 購読を解く札<T>(購読者List<T> 解く先の購読者List, Action<T> 購読者) : IDisposable
{
    private int 解除済み;

    public void Dispose()
    {
        if (Interlocked.Exchange(ref 解除済み, 1) == 0)
        {
            解く先の購読者List.外す(購読者);
        }
    }
}
