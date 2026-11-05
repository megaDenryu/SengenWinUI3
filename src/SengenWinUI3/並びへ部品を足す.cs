using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace SengenWinUI3;

// 横の並び・縦の並び・縦にスクロールする画面が共有する。実行時に決まる部品の並びは、StackPanel のコレクション初期化子へ広げて書けないため、1つずつ足す
internal static class 並びへ部品を足す
{
    public static StackPanel 部品を足す(this StackPanel 並び, IEnumerable<UIElement> 部品List)
    {
        foreach (var 部品 in 部品List) 並び.Children.Add(部品);
        return 並び;
    }
}
