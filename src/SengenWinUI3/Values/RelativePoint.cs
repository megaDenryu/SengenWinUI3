namespace SengenWinUI3;

/// <summary>塗る図形の外接する長方形の中の位置。左上が (0, 0)、右下が (1, 1) である。<see cref="LinearGradientBrush"/> の始点と終点に使う。</summary>
public readonly record struct RelativePoint(Ratio X, Ratio Y)
{
    internal Windows.Foundation.Point ToWinUI() => new(X.Value, Y.Value);
}
