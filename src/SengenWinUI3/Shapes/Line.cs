using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>始点と終点を結ぶ線。座標は部品の内側の Px で指定し、輪郭は Style で指定する。</summary>
public sealed class Line : Node
{
    private readonly WinUI.Shapes.Line 本物 = new();
    private readonly ElementValue<WinUI.Shapes.Line, Point> 始点;
    private readonly ElementValue<WinUI.Shapes.Line, Point> 終点;

    internal Line(Point start, Point end)
    {
        始点 = new(本物,
            static (本物, 値) => 本物.X1 == 値.X.Value && 本物.Y1 == 値.Y.Value,
            static (本物, 値) =>
            {
                if (本物.X1 != 値.X.Value) 本物.X1 = 値.X.Value;
                if (本物.Y1 != 値.Y.Value) 本物.Y1 = 値.Y.Value;
            });
        終点 = new(本物,
            static (本物, 値) => 本物.X2 == 値.X.Value && 本物.Y2 == 値.Y.Value,
            static (本物, 値) =>
            {
                if (本物.X2 != 値.X.Value) 本物.X2 = 値.X.Value;
                if (本物.Y2 != 値.Y.Value) 本物.Y2 = 値.Y.Value;
            });
        始点.Set(start);
        終点.Set(end);
    }

    internal override WinUI.FrameworkElement Element => 本物;

    /// <summary>始点を替える。</summary>
    public Line SetStart(Point start)
    {
        ThrowIfDestroyed();
        始点.Set(start);
        return this;
    }

    /// <summary>終点を替える。</summary>
    public Line SetEnd(Point end)
    {
        ThrowIfDestroyed();
        終点.Set(end);
        return this;
    }

    internal override void ApplyStyle(Style style) => ShapeStyle.All.Apply(style, 本物, this);
}
