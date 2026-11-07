using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// 複数の種類の部品が同じ形で当てる項目の当て方。1種類の部品だけが持つ項目は、その部品のファイルが当てる。
internal static class StyleApplication
{
    internal static void ThrowIfUnsupported(this Style style, StyleItems 持つ項目, Node 部品)
    {
        StyleItems 持たない項目 = 指定した項目(style) & ~持つ項目;
        if (持たない項目 != StyleItems.None)
        {
            throw new InvalidOperationException($"{部品.GetType().Name} は Style の {持たない項目} を持たない");
        }
    }

    internal static void ApplyLayout(this Style style, WinUI.FrameworkElement 本物)
    {
        if (style.Width is { } 幅) 本物.Width = 幅.Value;
        if (style.Height is { } 高さ) 本物.Height = 高さ.Value;
        if (style.MinWidth is { } 最小の幅) 本物.MinWidth = 最小の幅.Value;
        if (style.MaxWidth is { } 最大の幅) 本物.MaxWidth = 最大の幅.Value;
        if (style.MinHeight is { } 最小の高さ) 本物.MinHeight = 最小の高さ.Value;
        if (style.MaxHeight is { } 最大の高さ) 本物.MaxHeight = 最大の高さ.Value;
        if (style.Margin is { } 外側の余白) 本物.Margin = 外側の余白.ToWinUI();
        if (style.HorizontalAlignment is { } 横の寄せ方) 本物.HorizontalAlignment = 横の寄せ方.ToWinUI();
        if (style.VerticalAlignment is { } 縦の寄せ方) 本物.VerticalAlignment = 縦の寄せ方.ToWinUI();
        if (style.Opacity is { } 不透明度) 本物.Opacity = 不透明度.Value;
    }

    internal static void ApplyControlLook(this Style style, WinUI.Controls.Control 本物)
    {
        if (style.Padding is { } 内側の余白) 本物.Padding = 内側の余白.ToWinUI();
        if (style.BackgroundBrush() is { } 背景) 本物.Background = 背景;
        if (style.ForegroundBrush() is { } 文字の色) 本物.Foreground = 文字の色;
        if (style.BorderBrushBrush() is { } 枠線の色) 本物.BorderBrush = 枠線の色;
        if (style.BorderThickness is { } 枠線の太さ) 本物.BorderThickness = 枠線の太さ.ToWinUI();
        if (style.CornerRadius is { } 丸み) 本物.CornerRadius = new WinUI.CornerRadius(丸み.Value);
        if (style.FontSize is { } 文字の大きさ) 本物.FontSize = 文字の大きさ.Value;
        if (style.FontWeight is { } 文字の太さ) 本物.FontWeight = 文字の太さ.ToWinUI();
    }

    private static StyleItems 指定した項目(Style style) =>
        (style.Width is null ? StyleItems.None : StyleItems.Width)
        | (style.Height is null ? StyleItems.None : StyleItems.Height)
        | (style.MinWidth is null ? StyleItems.None : StyleItems.MinWidth)
        | (style.MaxWidth is null ? StyleItems.None : StyleItems.MaxWidth)
        | (style.MinHeight is null ? StyleItems.None : StyleItems.MinHeight)
        | (style.MaxHeight is null ? StyleItems.None : StyleItems.MaxHeight)
        | (style.Margin is null ? StyleItems.None : StyleItems.Margin)
        | (style.HorizontalAlignment is null ? StyleItems.None : StyleItems.HorizontalAlignment)
        | (style.VerticalAlignment is null ? StyleItems.None : StyleItems.VerticalAlignment)
        | (style.Opacity is null ? StyleItems.None : StyleItems.Opacity)
        | (style.Padding is null ? StyleItems.None : StyleItems.Padding)
        | (style.Orientation is null ? StyleItems.None : StyleItems.Orientation)
        | (style.Spacing is null ? StyleItems.None : StyleItems.Spacing)
        | (style.Background is null ? StyleItems.None : StyleItems.Background)
        | (style.Foreground is null ? StyleItems.None : StyleItems.Foreground)
        | (style.BorderBrush is null ? StyleItems.None : StyleItems.BorderBrush)
        | (style.BorderThickness is null ? StyleItems.None : StyleItems.BorderThickness)
        | (style.CornerRadius is null ? StyleItems.None : StyleItems.CornerRadius)
        | (style.FontSize is null ? StyleItems.None : StyleItems.FontSize)
        | (style.FontWeight is null ? StyleItems.None : StyleItems.FontWeight)
        | (style.TextWrapping is null ? StyleItems.None : StyleItems.TextWrapping)
        | (style.IsTextSelectionEnabled is null ? StyleItems.None : StyleItems.IsTextSelectionEnabled);
}
