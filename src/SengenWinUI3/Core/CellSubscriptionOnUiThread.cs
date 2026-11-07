using SengenWinUI3.State;

namespace SengenWinUI3;

// 部品の側のセルの購読(設計の文書の判断6)。知らせに付いた値は使わず、UI スレッドでそのときのセルの値を読み直して反映する。
// 同じスレッドの再入や別のスレッドの知らせの順が前後しても、最新の値で終わるためである。別のスレッドの知らせは UiThreadRequest が1回にまとめる。
internal sealed class CellSubscriptionOnUiThread<TValue> : IDisposable
{
    private readonly IReadOnlyCell<TValue> セル;
    private readonly Action<TValue> 値を部品へ反映する;
    private readonly IDisposable 購読を解く札;
    private volatile bool 解いたか;

    internal CellSubscriptionOnUiThread(IReadOnlyCell<TValue> セル, Action<TValue> 値を部品へ反映する)
    {
        this.セル = セル;
        this.値を部品へ反映する = 値を部品へ反映する;
        UiThreadRequest 今の値を反映する依頼 = new(UiThread.ForCurrentThread(), 今の値を反映する);
        購読を解く札 = セル.Subscribe(今の値を反映する依頼.Request); // 注意: 購読してから今の値を読む。逆にすると、読んだ後で購読する前の変化を取りこぼす
        try
        {
            今の値を反映する();
        }
        catch
        {
            購読を解く札.Dispose();
            throw;
        }
    }

    public void Dispose()
    {
        解いたか = true;
        購読を解く札.Dispose();
    }

    private void 今の値を反映する()
    {
        if (!解いたか)
        {
            値を部品へ反映する(セル.Value);
        }
    }
}
