using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// FrameworkElement が持つ項目の当て方。全部の葉の本物が FrameworkElement なので、全部の葉が使う。
internal static class FrameworkElementStyle
{
    internal static readonly IStyleGroup<WinUI.FrameworkElement> Layout = new StyleGroup<WinUI.FrameworkElement>(
        StyleItems.Width | StyleItems.Height | StyleItems.MinWidth | StyleItems.MaxWidth | StyleItems.MinHeight | StyleItems.MaxHeight
            | StyleItems.Margin | StyleItems.HorizontalAlignment | StyleItems.VerticalAlignment | StyleItems.Opacity,
        static (style, 本物) =>
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
        });
}
