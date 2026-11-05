using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace SengenWinUI3;

/// <summary>部品を縦に並べ、窓に収まらない分をスクロールで見せる画面。</summary>
public static class 縦に流れる画面
{
    private const double 並びの最大の幅 = 560; // 文の1行が長くなりすぎて読みにくくならない幅

    /// <summary>部品を縦に並べ、窓の左に寄せて置く。</summary>
    public static ScrollViewer From部品(params UIElement[] 部品List)
    {
        var 並び = 縦の並び.From部品(部品List);
        並び.Margin = new Thickness(24);
        並び.MaxWidth = 並びの最大の幅;
        並び.HorizontalAlignment = HorizontalAlignment.Left;
        return new ScrollViewer { Content = 並び };
    }
}
