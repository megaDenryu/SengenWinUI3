using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// Rectangle が持つ項目の当て方。角の丸みは RadiusX と RadiusY を同じ半径にする。
internal static class RectangleStyle
{
    internal static readonly IStyleGroup<WinUI.Shapes.Rectangle> Corners = new StyleGroup<WinUI.Shapes.Rectangle>(
        StyleItems.CornerRadius,
        static (見た目, 本物) =>
        {
            if (見た目.CornerRadius is { } 丸み)
            {
                本物.RadiusX = 丸み.Value;
                本物.RadiusY = 丸み.Value;
            }
        });

    internal static readonly StyleTarget<WinUI.Shapes.Rectangle> All = new(FrameworkElementStyle.Layout, ShapeStyle.Paint, ShapeStyle.StrokeForm, Corners);
}
