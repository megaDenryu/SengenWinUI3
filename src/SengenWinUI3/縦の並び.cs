using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace SengenWinUI3;

/// <summary>入力の欄や文のまとまりを、上から下へ並べる部品の並び。</summary>
public static class 縦の並び
{
    /// <summary>部品を上から順に、間を空けて並べる。</summary>
    public static StackPanel From部品(params UIElement[] 部品List) => 並びの組み立て.部品を足す(new StackPanel { Spacing = 12 }, 部品List);
}
