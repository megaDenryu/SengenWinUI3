namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>
    /// 見えている行だけを作って使い回す <see cref="SengenWinUI3.VirtualList{TItem, TRow}"/> を作る。スクロールは持たないので、<see cref="ScrollViewer(Node, Style?)"/> の中に置く。
    /// 行は <paramref name="createRow"/> で作る。項目は <c>SetItems</c> などで入れる。
    /// </summary>
    public static VirtualList<TItem, TRow> VirtualList<TItem, TRow>(Func<TRow> createRow, Style? style = null) where TRow : Component, IRecyclableRow<TItem> =>
        StyleIfGiven(new VirtualList<TItem, TRow>(createRow), style);
}
