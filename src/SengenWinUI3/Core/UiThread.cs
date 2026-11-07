using Microsoft.UI.Dispatching;

namespace SengenWinUI3;

// 部品を作った UI スレッドの出入口。今のスレッドがそのスレッドかを確かめる操作と、そのスレッドへ処理を送る操作を持つ。セルの購読もトーストもこれを使う。
// DispatcherQueue はスレッドごとに決まっているので、出入口もスレッドごとに1つ作って覚える。
internal sealed class UiThread
{
    [ThreadStatic]
    private static UiThread? このスレッドの出入口;

    private readonly DispatcherQueue 送り先;
    private readonly int 作ったスレッドのID;

    private UiThread(DispatcherQueue 送り先)
    {
        this.送り先 = 送り先;
        作ったスレッドのID = Environment.CurrentManagedThreadId;
    }

    // WinRT を呼ばずに、作ったスレッドの ID と比べる。
    internal bool IsCurrent => Environment.CurrentManagedThreadId == 作ったスレッドのID;

    internal static UiThread ForCurrentThread() =>
        このスレッドの出入口 ??= new(DispatcherQueue.GetForCurrentThread() ?? throw new InvalidOperationException("部品を作る操作とセルを結ぶ操作は、UI スレッドで行う"));

    // 注意: 待ち行列が閉じた後(ウインドウを閉じた後)は送れない。送る先の部品も無くなっているので、送れなかったことは捨てる。
    internal void Post(DispatcherQueueHandler UIスレッドで呼ぶ) => _ = 送り先.TryEnqueue(UIスレッドで呼ぶ);
}
