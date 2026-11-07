using Microsoft.UI.Dispatching;

namespace SengenWinUI3;

// 部品を作った UI スレッドの出入口。今のスレッドがそのスレッドかを確かめる操作と、そのスレッドへ処理を送る操作を持つ。セルの購読もトーストもこれを使う。
internal sealed class UiThread
{
    private readonly DispatcherQueue 送り先;

    private UiThread(DispatcherQueue 送り先) => this.送り先 = 送り先;

    internal bool IsCurrent => 送り先.HasThreadAccess;

    internal static UiThread ForCurrentThread() =>
        new(DispatcherQueue.GetForCurrentThread() ?? throw new InvalidOperationException("部品を作る操作とセルを結ぶ操作は、UI スレッドで行う"));

    // 注意: 待ち行列が閉じた後(ウインドウを閉じた後)は送れない。送る先の部品も無くなっているので、送れなかったことは捨てる。
    internal void Post(DispatcherQueueHandler 処理) => _ = 送り先.TryEnqueue(処理);
}
