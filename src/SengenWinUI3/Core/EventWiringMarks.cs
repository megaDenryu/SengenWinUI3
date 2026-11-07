namespace SengenWinUI3;

// 1つの部品で配線した出来事の名前の List。On〜 とそれに準じる1回だけの配線(StickToEnd・Flyout)の2回目を例外にする。全部の部品の出来事と葉だけの出来事を同じ形で調べる。
internal sealed class EventWiringMarks
{
    private List<string>? 配線済みの出来事List;

    internal void Mark(string 出来事の名前, string 部品の型の名前)
    {
        if (配線済みの出来事List is not null && 配線済みの出来事List.Contains(出来事の名前))
        {
            throw new InvalidOperationException($"{出来事の名前} はこの {部品の型の名前} で既に登録した。{出来事の名前} は1つの部品に1回だけ呼べる");
        }
        (配線済みの出来事List ??= []).Add(出来事の名前);
    }

    internal void Unmark(string 出来事の名前) => _ = 配線済みの出来事List?.Remove(出来事の名前);
}
