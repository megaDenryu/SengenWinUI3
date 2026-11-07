using System.Runtime.CompilerServices;
using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>アプリの Window に根の部品を置く拡張メソッド。</summary>
public static class WindowExtensions
{
    // 注意: Window は WinUI の型であり、拡張メソッドからフィールドを足せないので、ウインドウごとの今の根をこの表に持つ。ウインドウが回収されれば表からも消える。
    private static readonly ConditionalWeakTable<WinUI.Window, WindowRoot> 置いた根Map = new();

    /// <summary>根の部品をウインドウに置く。ウインドウが閉じたら根を破棄する。2回目は前の根を破棄して置き換える。親を持つ部品を渡すと例外を投げる。</summary>
    public static void SetContent(this WinUI.Window window, Node content)
    {
        content.ThrowIfOwned();
        if (置いた根Map.TryGetValue(window, out WindowRoot? 前の根))
        {
            前の根.DestroyRoot(); // 前の根を手放すときに、表から消える
        }
        置いた根Map.Add(window, new WindowRoot(window, content, () => 置いた根Map.Remove(window)));
    }
}
