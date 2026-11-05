using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace SengenWinUI3;

/// <summary>部品を縦に並べ、ウインドウに収まらない分をスクロールで見せる画面。</summary>
public static class 縦にスクロールする画面
{
    private const double 並びの最大の幅 = 560; // 文章の1行が長くなりすぎて読みにくくならない幅

    /// <summary>部品を縦に並べ、ウインドウの左に寄せて置く。</summary>
    public static ScrollViewer From部品(params UIElement[] 部品List) => new()
    {
        Content = new StackPanel { Spacing = 縦の並び.部品の間, Margin = new Thickness(24), MaxWidth = 並びの最大の幅, HorizontalAlignment = HorizontalAlignment.Left }.部品を足す(部品List),
    };
}
