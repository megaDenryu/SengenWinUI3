namespace SengenWinUI3;

/// <summary>左上の位置と大きさで表す長方形。名前は WinUI の Rect にそろえる。LayoutPanel の子を置く場所に使う。</summary>
public readonly record struct Rect(Px X, Px Y, Px Width, Px Height);
