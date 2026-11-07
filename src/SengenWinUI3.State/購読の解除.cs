namespace SengenWinUI3.State;

// 2回 Dispose しても1回目だけが購読者を外す。
internal sealed class 購読の解除<T>(購読者の一覧<T> 一覧, Action<T> 処理) : IDisposable
{
    private int 解除済み;

    public void Dispose()
    {
        if (Interlocked.Exchange(ref 解除済み, 1) == 0)
        {
            一覧.外す(処理);
        }
    }
}
