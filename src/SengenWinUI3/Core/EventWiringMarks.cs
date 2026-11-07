namespace SengenWinUI3;

// 1つの部品で受けた出来事の名前の台帳。On〜 とそれに準じる1回だけの配線(StickToEnd・Flyout)の2回目を例外にする。全部の部品の出来事と葉だけの出来事を同じ形で調べる。
internal sealed class EventWiringMarks
{
    private List<string>? 受けた出来事List;

    internal void Mark(string 出来事の名前, Node 部品)
    {
        if (受けた出来事List is not null && 受けた出来事List.Contains(出来事の名前))
        {
            throw new InvalidOperationException($"{部品.GetType().Name} の {出来事の名前} は受けてある。1つの部品に1回だけ呼べる");
        }
        (受けた出来事List ??= []).Add(出来事の名前);
    }
}
