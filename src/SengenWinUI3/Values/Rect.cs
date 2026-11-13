namespace SengenWinUI3;

/// <summary>左上の位置と大きさで表す長方形。名前と並びは WinUI の Rect にそろえる。Clip の範囲に使う。</summary>
public readonly record struct Rect(Px X, Px Y, Px Width, Px Height)
{
    internal Windows.Foundation.Rect ToWinUI() => new(X.Value, Y.Value, Width.Value, Height.Value);
}
