namespace SengenWinUI3.State;

/// <summary>元のセルが変わるたびに計算し直し、値が変わったときだけ購読者へ知らせるセル。</summary>
public sealed class DerivedCell<T> : IReadOnlyCell<T>, IDisposable
{
    private readonly Func<T> 計算する式;
    private readonly Cell<T> 結果;
    private readonly Lock 計算の排他 = new();
    private readonly IDisposable[] 元への購読List;
    private int 解除済み;

    /// <summary>compute が読むセルを、sources として全部渡す。</summary>
    public DerivedCell(Func<T> compute, params ICell[] sources)
    {
        計算する式 = compute;
        結果 = new Cell<T>(compute());
        元への購読List = Array.ConvertAll(sources, 元のセル => 元のセル.Subscribe(再計算する));
        再計算する(); // 初めの計算と購読の間に元が変わっていても取りこぼさない
    }

    public T Value => 結果.Value;

    public IDisposable Subscribe(Action onChanged) => 結果.Subscribe(onChanged);

    public IDisposable Subscribe(Action<T> onChanged) => 結果.Subscribe(onChanged);

    /// <summary>元のセルへの購読を解く。2回呼んでも壊れない。</summary>
    public void Dispose()
    {
        if (Interlocked.Exchange(ref 解除済み, 1) == 1)
        {
            return;
        }
        foreach (IDisposable 購読 in 元への購読List)
        {
            購読.Dispose();
        }
    }

    // 注意: 計算と設定を排他で囲むのは、別のスレッドで古い元の値から出した結果が、新しい結果を上書きしないため。
    // 結果への設定が購読者へ知らせるので、購読者が別のスレッドを待つ処理を書くと止まる。
    private void 再計算する()
    {
        if (Volatile.Read(ref 解除済み) == 1)
        {
            return;
        }
        lock (計算の排他)
        {
            結果.Value = 計算する式();
        }
    }
}
