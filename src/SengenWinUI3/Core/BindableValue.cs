using SengenWinUI3.State;

namespace SengenWinUI3;

// 値を見せる部品の、見せる値の出どころ(設計の文書の判断13)。固定の値を Set で替えるか、セルに結ぶかの、どちらか1つにする。
// 本物の今の値と等しい値は本物へ設定しない。比べる相手を手元の写しでなく本物にするのは、利用者の入力で本物が変わる部品でも、画面とセルが食い違わないためである。
internal sealed class BindableValue<TTarget, TValue>(Node 部品, TTarget 設定する先, Func<TTarget, TValue, bool> 本物と等しいか, Action<TTarget, TValue> 本物へ設定する)
{
    private bool セルに結んだか;

    internal void Set(TValue 値)
    {
        if (セルに結んだか)
        {
            throw new InvalidOperationException($"セルに結んだ {部品.GetType().Name} の値は、セルの値を変えて替える");
        }
        等しくなければ設定する(値);
    }

    internal void Bind(IReadOnlyCell<TValue> セル)
    {
        if (セルに結んだか)
        {
            throw new InvalidOperationException($"{部品.GetType().Name} はすでにセルに結んである");
        }
        セルに結んだか = true;
        _ = 部品.SubscribeCell(セル, 等しくなければ設定する);
    }

    private void 等しくなければ設定する(TValue 値)
    {
        if (!本物と等しいか(設定する先, 値))
        {
            本物へ設定する(設定する先, 値);
        }
    }
}
