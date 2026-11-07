using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>四辺の長さ。余白と枠線の太さに使う。</summary>
public readonly record struct Thickness(Px Left, Px Top, Px Right, Px Bottom)
{
    /// <summary>四辺を同じ長さにする。</summary>
    public Thickness(Px uniform) : this(uniform, uniform, uniform, uniform) { }

    /// <summary>上と下を <paramref name="vertical"/>、左と右を <paramref name="horizontal"/> にする。</summary>
    public Thickness(Px vertical, Px horizontal) : this(horizontal, vertical, horizontal, vertical) { }

    internal WinUI.Thickness ToWinUI() => new(Left.Value, Top.Value, Right.Value, Bottom.Value);
}
