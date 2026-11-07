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

// 入力の部品が、本物への設定・InputValue の組み立て・初期値の設定を1文で済ませるための作成口。
internal static class InputValue
{
    // 初期値を InputValue と本物へ1回だけ渡させる。初期値の設定が例外なら、そのまま投げる。
    internal static InputValue<TValue> Create<TTarget, TValue>(
        TTarget 設定する先,
        TValue 初期値,
        Func<TTarget, TValue, bool> 本物と等しいか,
        Action<TTarget, TValue> 本物へ設定する)
    {
        ElementValue<TTarget, TValue> 設定先 = new(設定する先, 本物と等しいか, 本物へ設定する);
        InputValue<TValue> 入力値 = new(初期値, 設定先.Set);
        入力値.Set(初期値);
        return 入力値;
    }
}
