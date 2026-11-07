using Microsoft.UI.Dispatching;
using SengenWinUI3.State;

namespace SengenWinUI3;

// 部品の側のセルの購読(設計の文書の判断6)。知らせを受けたスレッドが部品の UI スレッドでなければ UI スレッドへ送り、送った先でセルの値を読み直す。
// 届いた順が前後しても、最新の値で終わるためである。知らせのたびに走る処理では、ラムダも配列も作らない。
internal sealed class CellSubscriptionOnUiThread<V> : IDisposable
{
    private readonly Node 部品;
    private readonly IReadOnlyCell<V> セル;
    private readonly Action<V> 反映する;
    private readonly DispatcherQueue 送り先;
    private readonly DispatcherQueueHandler 送った先で反映する;
    private readonly IDisposable 解除;

    internal CellSubscriptionOnUiThread(Node 部品, IReadOnlyCell<V> セル, Action<V> 反映する)
    {
        this.部品 = 部品;
        this.セル = セル;
        this.反映する = 反映する;
        送り先 = DispatcherQueue.GetForCurrentThread() ?? throw new InvalidOperationException("部品へセルを結ぶのは、部品を作った UI スレッドで行う");
        送った先で反映する = 今の値を反映する;
        解除 = セル.Subscribe(知らせを受ける); // 注意: 購読してから今の値を読む。逆にすると、読んだ後で購読する前の変化を取りこぼす
        反映する(セル.Value);
    }

    public void Dispose() => 解除.Dispose();

    private void 知らせを受ける(V 値)
    {
        if (!送り先.HasThreadAccess)
        {
            _ = 送り先.TryEnqueue(送った先で反映する);
            return;
        }
        if (!部品.IsDestroyed)
        {
            反映する(値);
        }
    }

    private void 今の値を反映する()
    {
        if (!部品.IsDestroyed)
        {
            反映する(セル.Value);
        }
    }
}
