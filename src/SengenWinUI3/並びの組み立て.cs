using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace SengenWinUI3;

// 横の並びと縦の並びが共有する、部品を順に足す処理。StackPanel の Children はコレクション初期化子でしか一度に足せず、並びを引数で受けると1つずつ足すことになるため
internal static class 並びの組み立て
{
    public static StackPanel 部品を足す(StackPanel 並び, IEnumerable<UIElement> 部品List)
    {
        foreach (var 部品 in 部品List) 並び.Children.Add(部品);
        return 並び;
    }
}
