namespace SengenWinUI3;

// 部品の破棄で一緒に解くもの(セルの購読・出来事の購読・一緒に破棄する部品)の台帳。
internal sealed class CleanupList
{
    private List<IDisposable>? 後片付けの対象List;

    internal void Add(IDisposable 後片付けの対象) => (後片付けの対象List ??= []).Add(後片付けの対象);

    // 返した値を Dispose すると、部品の破棄を待たずに解き、台帳からも外す。使い回す行が項目ごとに結び直しても、台帳が伸び続けないためである。
    internal IDisposable AddRemovable(IDisposable 後片付けの対象)
    {
        Add(後片付けの対象);
        return new 先に解く札(this, 後片付けの対象);
    }

    internal void DisposeAll(CollectedExceptions 起きた例外)
    {
        List<IDisposable>? 解く対象List = 後片付けの対象List;
        後片付けの対象List = null;
        foreach (IDisposable 後片付けの対象 in 解く対象List ?? [])
        {
            起きた例外.Run(後片付けの対象.Dispose);
        }
    }

    private void 外す(IDisposable 後片付けの対象) => _ = 後片付けの対象List?.Remove(後片付けの対象);

    private sealed class 先に解く札(CleanupList 台帳, IDisposable 後片付けの対象) : IDisposable
    {
        private bool 解いたか;

        public void Dispose()
        {
            if (解いたか)
            {
                return;
            }
            解いたか = true;
            台帳.外す(後片付けの対象);
            後片付けの対象.Dispose();
        }
    }
}
