namespace SengenWinUI3;

/// <summary>左上の位置と大きさで表す長方形。名前は WinUI の Rect にそろえる。LayoutPanel の子を置く場所と、Clip の切り抜きの範囲に使う。</summary>
public readonly record struct Rect(Px X, Px Y, Px Width, Px Height)
{
    internal Windows.Foundation.Rect ToWinUI() => new(X.Value, Y.Value, Width.Value, Height.Value);
}
