namespace SengenWinUI3.State;

// 購読者の配列は、購読と解除のたびに新しい配列へ差し替える。知らせる側は差し替える前の配列を読むので、
// 知らせの途中の購読と解除と別のスレッドの通知で、走査中の配列が壊れない。
internal sealed class 購読者の一覧<T>
{
    private readonly Lock 差し替えの排他 = new();
    private Action<T>[] 購読者List = [];

    public IDisposable 加える(Action<T> 処理)
    {
        lock (差し替えの排他)
        {
            購読者List = [.. 購読者List, 処理];
        }
        return new 購読の解除<T>(this, 処理);
    }

    public void 外す(Action<T> 処理)
    {
        lock (差し替えの排他)
        {
            int 位置 = 最初の位置を探す(処理);
            if (位置 < 0)
            {
                return;
            }
            購読者List = [.. 購読者List[..位置], .. 購読者List[(位置 + 1)..]];
        }
    }

    // 注意: 購読者が投げた例外で残りの購読者への知らせを止めないよう、全員へ知らせてからまとめて投げる。
    public void 知らせる(T 値)
    {
        Action<T>[] 知らせる先List = Volatile.Read(ref 購読者List);
        List<Exception>? 例外List = null;
        foreach (Action<T> 処理 in 知らせる先List)
        {
            try
            {
                処理(値);
            }
            catch (Exception 例外)
            {
                例外List ??= new List<Exception>();
                例外List.Add(例外);
            }
        }
        if (例外List is not null)
        {
            throw new AggregateException(例外List);
        }
    }

    private int 最初の位置を探す(Action<T> 処理)
    {
        for (int 位置 = 0; 位置 < 購読者List.Length; 位置++)
        {
            if (ReferenceEquals(購読者List[位置], 処理))
            {
                return 位置;
            }
        }
        return -1;
    }
}
