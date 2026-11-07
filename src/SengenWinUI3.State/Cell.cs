namespace SengenWinUI3.State;

/// <summary>値を1つ持ち、前と違う値に変わったときだけ購読者へ知らせるセル。</summary>
public sealed class Cell<T>(T initialValue) : IReadOnlyCell<T>
{
    private readonly Lock 値の排他 = new();
    private readonly 購読者の一覧<T> 購読者の一覧 = new();
    private T 現在の値 = initialValue;

    /// <summary>
    /// 値を読み書きする。どのスレッドから設定してもよい。前の値と等しければ知らせない。
    /// 知らせは設定したスレッドで同期して行う。
    /// </summary>
    public T Value
    {
        get
        {
            lock (値の排他)
            {
                return 現在の値;
            }
        }
        set
        {
            lock (値の排他)
            {
                if (EqualityComparer<T>.Default.Equals(現在の値, value))
                {
                    return;
                }
                現在の値 = value;
            }
            購読者の一覧.知らせる(value);
        }
    }

    public IDisposable Subscribe(Action onChanged) => 購読者の一覧.加える(_ => onChanged());

    public IDisposable Subscribe(Action<T> onChanged) => 購読者の一覧.加える(onChanged);
}
