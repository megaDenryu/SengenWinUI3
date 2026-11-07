namespace SengenWinUI3;

// 比べる相手を本物にするので、利用者の入力で値が変わる部品にも使える。表示する値のコピーは持たない。
internal sealed class ElementValue<TTarget, TValue>(TTarget 設定する先, Func<TTarget, TValue, bool> 本物と等しいか, Action<TTarget, TValue> 本物へ設定する)
{
    internal void Set(TValue 値)
    {
        if (!本物と等しいか(設定する先, 値))
        {
            本物へ設定する(設定する先, 値);
        }
    }
}
