using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// Grid が固有に持つ項目。背景は PanelStyle.Background を使う。
internal static class GridStyle
{
    internal static readonly IStyleGroup<WinUI.Controls.Grid> Padding = new StyleGroup<WinUI.Controls.Grid>(
        StyleItems.Padding,
        static (見た目, 本物) =>
        {
            if (見た目.Padding is { } 内側の余白) 本物.Padding = 内側の余白.ToWinUI();
        });

    internal static readonly IStyleGroup<WinUI.Controls.Grid> Border = new StyleGroup<WinUI.Controls.Grid>(
        StyleItems.BorderBrush | StyleItems.BorderThickness | StyleItems.CornerRadius,
        static (見た目, 本物) =>
        {
            if (見た目.CachedBorderBrush() is { } 枠線の色) 本物.BorderBrush = 枠線の色;
            if (見た目.BorderThickness is { } 枠線の太さ) 本物.BorderThickness = 枠線の太さ.ToWinUI();
            if (見た目.CornerRadius is { } 丸み) 本物.CornerRadius = new WinUI.CornerRadius(丸み.Value);
        });

    internal static readonly IStyleGroup<WinUI.Controls.Grid> Arrangement = new StyleGroup<WinUI.Controls.Grid>(
        StyleItems.RowSpacing | StyleItems.ColumnSpacing,
        static (見た目, 本物) =>
        {
            if (見た目.RowSpacing is { } 行の間隔) 本物.RowSpacing = 行の間隔.Value;
            if (見た目.ColumnSpacing is { } 列の間隔) 本物.ColumnSpacing = 列の間隔.Value;
        });

    internal static readonly StyleTarget<WinUI.Controls.Grid> All = new(FrameworkElementStyle.Layout, PanelStyle.Background, Padding, Border, Arrangement);
}
