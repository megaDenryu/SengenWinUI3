using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// StackPanel が持つ項目の当て方。WinUI の StackPanel は余白と枠を Panel から受け継がず、自分で持つ。
internal static class StackPanelStyle
{
    internal static readonly IStyleGroup<WinUI.Controls.StackPanel> Padding = new StyleGroup<WinUI.Controls.StackPanel>(
        StyleItems.Padding,
        static (style, 本物) =>
        {
            if (style.Padding is { } 内側の余白) 本物.Padding = 内側の余白.ToWinUI();
        });

    internal static readonly IStyleGroup<WinUI.Controls.StackPanel> Border = new StyleGroup<WinUI.Controls.StackPanel>(
        StyleItems.BorderBrush | StyleItems.BorderThickness | StyleItems.CornerRadius,
        static (style, 本物) =>
        {
            if (style.CachedBorderBrush() is { } 枠線の色) 本物.BorderBrush = 枠線の色;
            if (style.BorderThickness is { } 枠線の太さ) 本物.BorderThickness = 枠線の太さ.ToWinUI();
            if (style.CornerRadius is { } 丸み) 本物.CornerRadius = new WinUI.CornerRadius(丸み.Value);
        });

    internal static readonly IStyleGroup<WinUI.Controls.StackPanel> Arrangement = new StyleGroup<WinUI.Controls.StackPanel>(
        StyleItems.Orientation | StyleItems.Spacing,
        static (style, 本物) =>
        {
            if (style.Orientation is { } 向き) 本物.Orientation = 向き.ToWinUI();
            if (style.Spacing is { } 間隔) 本物.Spacing = 間隔.Value;
        });
}
