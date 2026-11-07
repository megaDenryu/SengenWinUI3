using SengenWinUI3.State;

namespace SengenWinUI3;

/// <summary>
/// 結ぶセルを替えられる、セルの見張り。<see cref="Component"/> の WatchCell で作り、部品の破棄で解く。
/// 結んだ時に1回、セルの値が変わるたびに、作るときに渡した処理を UI スレッドで呼ぶ。知らせに付いた値は使わず、UI スレッドでそのときのセルの値を読み直す。
/// UI スレッドへの送りとセルへ渡す処理は作るときに1回だけ作るので、使い回す行が項目ごとに <see cref="Watch"/> し直しても、割り当てはセルの購読の札だけである。
/// </summary>
public sealed class CellWatcher<TValue>
{
    private readonly Node 部品;
    private readonly Action<TValue> 値を部品へ反映する;
    private readonly UiThreadRequest 今の値を反映する依頼;
    private readonly Action<TValue> 知らせを受けたら反映を頼む;
    private IReadOnlyCell<TValue>? 見張るセル; // 結んでいる間だけある。UI スレッドでだけ読み書きする
    private IDisposable? 購読を解く札;

    internal CellWatcher(Node 部品, Action<TValue> 値を部品へ反映する)
    {
        this.部品 = 部品;
        this.値を部品へ反映する = 値を部品へ反映する;
        今の値を反映する依頼 = new UiThreadRequest(UiThread.ForCurrentThread(), 今の値を反映する);
        知らせを受けたら反映を頼む = _ => 今の値を反映する依頼.Request();
    }

    /// <summary>前のセルの購読を解いてから <paramref name="cell"/> に結び、今の値で1回反映する。UI スレッドで呼ぶ。</summary>
    public void Watch(IReadOnlyCell<TValue> cell)
    {
        部品.ThrowIfDestroyed();
        Unwatch();
        購読を解く札 = cell.Subscribe(知らせを受けたら反映を頼む); // 注意: 購読してから今の値を読む。逆にすると、読んだ後で購読する前の変化を取りこぼす
        見張るセル = cell;
        try
        {
            今の値を反映する();
        }
        catch
        {
            Unwatch();
            throw;
        }
    }

    /// <summary>今のセルの購読を解く。結んでいなければ何もしない。</summary>
    public void Unwatch()
    {
        見張るセル = null;
        購読を解く札?.Dispose();
        購読を解く札 = null;
    }

    // 送った先で、前のセルの知らせが遅れて届いても、今結んでいるセルの値を読むので、最新の値で終わる。
    private void 今の値を反映する()
    {
        if (見張るセル is { } セル)
        {
            値を部品へ反映する(セル.Value);
        }
    }
}
