using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace SengenWinUI3;

/// <summary>入力の欄や文章のまとまりを、上から下へ並べる部品の並び。</summary>
public static class 縦の並び
{
    internal const double 部品の間 = 12; // 縦にスクロールする画面も、同じ間で並べる

    /// <summary>部品を上から順に、間を空けて並べる。</summary>
    public static StackPanel From部品(params UIElement[] 部品List) => new StackPanel { Spacing = 部品の間 }.部品を足す(部品List);
}
