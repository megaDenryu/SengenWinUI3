namespace SengenWinUI3;

// 通知が Set の後に届く場合も、最後にプログラムが設定した値を通知し直さない。
internal sealed class InputValue<TValue>(TValue initialValue, Action<TValue> setValue)
{
    private TValue 最後に設定した値 = initialValue;

    internal void Set(TValue value)
    {
        TValue 前の値 = 最後に設定した値;
        最後に設定した値 = value; // 設定中に同期して届く通知も同じ比較で除く
        try
        {
            setValue(value);
        }
        catch
        {
            最後に設定した値 = 前の値;
            throw;
        }
    }

    // 古い出来事の引数ではなく、本物から読み直した現在値を渡す。
    internal bool IsUserChange(TValue currentValue) => !EqualityComparer<TValue>.Default.Equals(最後に設定した値, currentValue);
}
