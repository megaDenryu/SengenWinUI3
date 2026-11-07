using Microsoft.UI.Dispatching;

namespace SengenWinUI3;

// トーストが時間で消えるための時計。UI スレッドで作って使う。
internal sealed class ToastExpiry
{
    private readonly DispatcherQueueTimer 時計 = DispatcherQueue.GetForCurrentThread().CreateTimer();

    // 閉じられるまで残る消え方では、時計を動かさない。
    internal void StartFor(ToastLifetime lifetime, Action expired) => _ = lifetime switch
    {
        TimedLifetime 時間 => 時間が過ぎたら呼ぶ(時間.Duration, expired),
        UntilClosedLifetime => false,
    };

    internal void Stop() => 時計.Stop();

    private bool 時間が過ぎたら呼ぶ(TimeSpan 時間, Action expired)
    {
        時計.Interval = 時間;
        時計.IsRepeating = false;
        時計.Tick += (_, _) =>
        {
            時計.Stop();
            expired();
        };
        時計.Start();
        return true;
    }
}
