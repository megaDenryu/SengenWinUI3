using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>始点と終点を結ぶ線。座標は部品の内側の Px で指定し、輪郭は Style で指定する。</summary>
public sealed class Line : Node
{
    private readonly WinUI.Shapes.Line 本物 = new();
    private readonly ElementValue<WinUI.Shapes.Line, Px> 始点の横;
    private readonly ElementValue<WinUI.Shapes.Line, Px> 始点の縦;
    private readonly ElementValue<WinUI.Shapes.Line, Px> 終点の横;
    private readonly ElementValue<WinUI.Shapes.Line, Px> 終点の縦;

    internal Line(Px x1, Px y1, Px x2, Px y2)
    {
        始点の横 = new(本物, static (本物, 値) => 本物.X1 == 値.Value, static (本物, 値) => 本物.X1 = 値.Value);
        始点の横.Set(x1);
        始点の縦 = new(本物, static (本物, 値) => 本物.Y1 == 値.Value, static (本物, 値) => 本物.Y1 = 値.Value);
        始点の縦.Set(y1);
        終点の横 = new(本物, static (本物, 値) => 本物.X2 == 値.Value, static (本物, 値) => 本物.X2 = 値.Value);
        終点の横.Set(x2);
        終点の縦 = new(本物, static (本物, 値) => 本物.Y2 == 値.Value, static (本物, 値) => 本物.Y2 = 値.Value);
        終点の縦.Set(y2);
    }

    internal override WinUI.FrameworkElement Element => 本物;

    /// <summary>始点の横座標を替える。</summary>
    public Line SetX1(Px x1)
    {
        ThrowIfDestroyed();
        始点の横.Set(x1);
        return this;
    }

    /// <summary>始点の縦座標を替える。</summary>
    public Line SetY1(Px y1)
    {
        ThrowIfDestroyed();
        始点の縦.Set(y1);
        return this;
    }

    /// <summary>終点の横座標を替える。</summary>
    public Line SetX2(Px x2)
    {
        ThrowIfDestroyed();
        終点の横.Set(x2);
        return this;
    }

    /// <summary>終点の縦座標を替える。</summary>
    public Line SetY2(Px y2)
    {
        ThrowIfDestroyed();
        終点の縦.Set(y2);
        return this;
    }

    internal override void ApplyStyle(Style style) => ShapeStyle.All.Apply(style, 本物, this);
}
