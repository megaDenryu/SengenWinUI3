namespace SengenWinUI3;

/// <summary>
/// <see cref="VirtualList{TItem, TRow}"/> が使い回す行。行は作るときに1回だけ配線し、使い回されるたびに <see cref="Show"/> で別の項目を受ける。
/// 行は <see cref="Component"/> の派生にする。項目の中のセルを見せる行は、コンストラクタで <c>WatchCell</c> の見張りを作ってフィールドに持ち、<see cref="Show"/> で項目のセルを Watch する。
/// 一覧は <see cref="Release"/> を呼ぶ前に行の全部の見張りを解くので、<see cref="Release"/> で書くのは、項目の外の状態を戻すことだけである。
/// </summary>
public interface IRecyclableRow<in TItem>
{
    /// <summary>この項目を見せる。前の項目からは <see cref="Release"/> で離れてある。</summary>
    void Show(TItem item);

    /// <summary>今の項目から離れる。行が使い回しの待ちへ入るときと、一覧の破棄のときに、行の見張りを解いた後で呼ばれる。</summary>
    void Release();
}
