using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>楕円を表示する部品。大きさ・塗り・輪郭は Style で指定する。</summary>
public sealed class Ellipse : Node
{
    private readonly WinUI.Shapes.Ellipse 本物 = new();

    internal Ellipse() { }

    internal override WinUI.FrameworkElement Element => 本物;

    internal override void ApplyStyle(Style style) => ShapeStyle.All.Apply(style, 本物, this);
}
