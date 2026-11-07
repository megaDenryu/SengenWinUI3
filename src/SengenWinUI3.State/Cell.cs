namespace SengenWinUI3.State;

/// <summary>値を1つ持ち、前と違う値に変わったときだけ購読者へ知らせるセル。</summary>
public sealed class Cell<T>(T initialValue) : IReadOnlyCell<T>
{
    private readonly Lock 値のロック = new();
    private readonly 購読者List<T> 購読者List = new();
    private T 現在の値 = initialValue;

    /// <summary>
    /// 値を読み書きする。どのスレッドから設定してもよい。前の値と等しければ知らせない。
    /// 知らせは設定したスレッドで同期して行う。
    /// </summary>
    public T Value
    {
        get
        {
            lock (値のロック)
            {
                return 現在の値;
            }
        }
        set
        {
            lock (値のロック)
            {
                if (EqualityComparer<T>.Default.Equals(現在の値, value))
                {
                    return;
                }
                現在の値 = value;
            }
            購読者List.知らせる(value);
        }
    }

    public IDisposable Subscribe(Action onChanged) => 購読者List.加える(_ => onChanged());

    public IDisposable Subscribe(Action<T> onChanged) => 購読者List.加える(onChanged);
}
