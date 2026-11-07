namespace SengenWinUI3;

/// <summary>
/// <see cref="VirtualList{TItem, TRow}"/> が使い回す行。行は作るときに1回だけ配線し、使い回されるたびに <see cref="Show"/> で別の項目を受ける。
/// 行は <see cref="Component"/> の派生にし、Show で値を設定する。行の作者は Release で項目に結び付いた資源を解く。
/// </summary>
public interface IRecyclableRow<in TItem>
{
    /// <summary>この項目を見せる。<see cref="Show"/> の前に、前の項目は <see cref="Release"/> で解いてある。</summary>
    void Show(TItem item);

    /// <summary>今の項目から離れる。使い回しの待ちへ入るときと一覧の破棄のときに呼ばれる。項目に結び付いた資源はここで解く。</summary>
    void Release();
}
