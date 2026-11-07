namespace SengenWinUI3;

/// <summary>
/// <see cref="VirtualList{TItem, TRow}"/> が使い回す行。行は作るときに1回だけ配線し、使い回されるたびに <see cref="Show"/> で別の項目を受ける。
/// 項目の中のセルを見せる行は、<see cref="Show"/> で <c>Component.Observe</c> を呼んで札を持ち、<see cref="Release"/> で札を Dispose する。
/// </summary>
public interface IRecyclableRow<in TItem>
{
    /// <summary>この項目を見せる。前の項目からは <see cref="Release"/> で離れてある。</summary>
    void Show(TItem item);

    /// <summary>今の項目から離れる。行が使い回しの待ちへ入るときと、一覧の破棄のときに呼ばれる。</summary>
    void Release();
}
