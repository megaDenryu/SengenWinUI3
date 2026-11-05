using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace SengenWinUI3;

/// <summary>ボタンの組のように、1行に横へ並べる部品の並び。</summary>
public static class 横の並び
{
    /// <summary>部品を左から順に、間を空けて並べる。</summary>
    public static StackPanel From部品(params UIElement[] 部品List) => new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8 }.部品を足す(部品List);
}
