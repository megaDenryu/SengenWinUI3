namespace SengenWinUI3;

// 部品か浮かぶ部品の破棄で一緒に解くもの(出来事の購読・一緒に破棄する部品)の List。
internal sealed class CleanupList
{
    private List<IDisposable>? 後片付けの対象List;

    internal void Add(IDisposable 後片付けの対象) => (後片付けの対象List ??= []).Add(後片付けの対象);

    // 返した値を Dispose すると、破棄を待たずに解き、後片付けList からも外す。浮かぶ部品だけを先に破棄したとき、付けた先の後片付けList に残さないためである。
    // 後片付けList には札そのものを入れるので、先に解いた後に破棄で解いても、破棄で解いた後に札を解いても、対象は1回だけ解ける。
    internal IDisposable AddRemovable(IDisposable 後片付けの対象)
    {
        先に解く札 札 = new(this, 後片付けの対象);
        Add(札);
        return 札;
    }

    internal void DisposeAll(CollectedExceptions 集めた例外)
    {
        List<IDisposable>? 解く対象List = 後片付けの対象List;
        後片付けの対象List = null;
        foreach (IDisposable 後片付けの対象 in 解く対象List ?? [])
        {
            集めた例外.Run(後片付けの対象.Dispose);
        }
    }

    private void 外す(IDisposable 後片付けの対象) => _ = 後片付けの対象List?.Remove(後片付けの対象);

    private sealed class 先に解く札(CleanupList 後片付けList, IDisposable 後片付けの対象) : IDisposable
    {
        private bool 解いたか;

        public void Dispose()
        {
            if (解いたか)
            {
                return;
            }
            解いたか = true;
            後片付けList.外す(this);
            後片付けの対象.Dispose();
        }
    }
}
