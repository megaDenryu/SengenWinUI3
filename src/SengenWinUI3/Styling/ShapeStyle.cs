using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

internal static class ShapeStyle
{
    internal static readonly IStyleGroup<WinUI.Shapes.Shape> Paint = new StyleGroup<WinUI.Shapes.Shape>(
        StyleItems.Fill | StyleItems.Stroke | StyleItems.StrokeThickness,
        static (見た目, 本物) =>
        {
            if (見た目.CachedFill() is { } 塗り) 本物.Fill = 塗り;
            if (見た目.CachedStroke() is { } 輪郭) 本物.Stroke = 輪郭;
            if (見た目.StrokeThickness is { } 太さ) 本物.StrokeThickness = 太さ.Value;
        });

    internal static readonly StyleTarget<WinUI.Shapes.Shape> All = new(FrameworkElementStyle.Layout, Paint);
}
