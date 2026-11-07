using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>SVG の道の文字列で書いた輪郭を描く部品。塗り・輪郭・線の端と角の形は Style で指定する。作る関数は <see cref="UI.Path(PathData, Style?)"/>。</summary>
public sealed class Path : Node
{
    private readonly WinUI.Shapes.Path 本物 = new();

    internal Path(PathData data) => 本物.Data = data.ToWinUI();

    internal override WinUI.FrameworkElement Element => 本物;

    /// <summary>輪郭を替える。</summary>
    public Path SetData(PathData data)
    {
        ThrowIfDestroyed();
        本物.Data = data.ToWinUI();
        return this;
    }

    internal override void ApplyStyle(Style style) => ShapeStyle.All.Apply(style, 本物, this);
}
