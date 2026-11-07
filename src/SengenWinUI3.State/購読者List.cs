namespace SengenWinUI3.State;

// 購読者の配列は、購読と解除のたびに新しい配列へ差し替える。知らせる側は差し替える前の配列を読むので、
// 知らせの途中の購読と解除と別のスレッドの通知で、走査中の配列が壊れない。
internal sealed class 購読者List<T>
{
    private readonly Lock 差し替えのロック = new();
    private Action<T>[] 登録済みList = [];

    public IDisposable 加える(Action<T> 購読者)
    {
        lock (差し替えのロック)
        {
            登録済みList = [.. 登録済みList, 購読者];
        }
        return new 購読を解く札<T>(this, 購読者);
    }

    public void 外す(Action<T> 購読者)
    {
        lock (差し替えのロック)
        {
            int 位置 = 最初の位置を探す(購読者);
            if (位置 < 0)
            {
                return;
            }
            登録済みList = [.. 登録済みList[..位置], .. 登録済みList[(位置 + 1)..]];
        }
    }

    // 注意: 購読者が投げた例外で残りの購読者への知らせを止めないよう、全員へ知らせてからまとめて投げる。
    public void 知らせる(T 値)
    {
        Action<T>[] 知らせる先List = Volatile.Read(ref 登録済みList);
        List<Exception>? 例外List = null;
        foreach (Action<T> 購読者 in 知らせる先List)
        {
            try
            {
                購読者(値);
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

    private int 最初の位置を探す(Action<T> 購読者)
    {
        for (int 位置 = 0; 位置 < 登録済みList.Length; 位置++)
        {
            if (ReferenceEquals(登録済みList[位置], 購読者))
            {
                return 位置;
            }
        }
        return -1;
    }
}
