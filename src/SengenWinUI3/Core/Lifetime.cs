namespace SengenWinUI3;

// 部品か浮かぶ部品1つの寿命。破棄したか・破棄で解く後片付け・1回だけの配線の印を持ち、破棄した後の操作を拒む。Node と FloatingNode が1つずつ持つ。
internal sealed class Lifetime(string 持ち主の型の名前)
{
    private readonly CleanupList 後片付けList = new();
    private readonly EventWiringMarks 配線済みの出来事 = new();

    internal bool IsDestroyed { get; private set; }

    internal void ThrowIfDestroyed()
    {
        if (IsDestroyed)
        {
            throw new InvalidOperationException($"{持ち主の型の名前} は破棄した後に操作された");
        }
    }

    internal void OwnDisposable(IDisposable resource)
    {
        ThrowIfDestroyed();
        後片付けList.Add(resource);
    }

    // 返した値を Dispose すると先に解ける。Dispose しなくても破棄で解ける。
    internal IDisposable OwnRemovable(IDisposable resource)
    {
        ThrowIfDestroyed();
        return 後片付けList.AddRemovable(resource);
    }

    internal void MarkWiredOnce(string eventName)
    {
        ThrowIfDestroyed();
        配線済みの出来事.Mark(eventName, 持ち主の型の名前);
    }

    // 1回だけの配線を外したとき(付けた Flyout だけを破棄したとき)に印を戻し、また配線できるようにする。破棄の途中からも呼ばれる。
    internal void UnmarkWired(string eventName) => 配線済みの出来事.Unmark(eventName);

    // 注意: 破棄の印を付けてから後片付けを解く。後片付けの中から持ち主の Destroy が再び呼ばれても、2回目として何もしないためである。
    internal void Destroy(CollectedExceptions 集めた例外)
    {
        IsDestroyed = true;
        後片付けList.DisposeAll(集めた例外);
    }
}
