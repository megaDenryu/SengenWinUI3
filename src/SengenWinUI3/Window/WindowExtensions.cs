using System.Runtime.CompilerServices;
using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>アプリの Window に根の部品を置く拡張メソッド。</summary>
public static class WindowExtensions
{
    // 注意: Window は WinUI の型であり、拡張メソッドからフィールドを足せないので、ウインドウごとの状態の持ち主をこの表に持つ。表はこれ1つにし、ウインドウに付ける状態は WindowHost に足す。ウインドウが回収されれば表からも消える。
    private static readonly ConditionalWeakTable<WinUI.Window, WindowHost> ウインドウの持ち主Map = new();

    /// <summary>根の部品をウインドウに置く。ウインドウが閉じたら根を破棄する。2回目は前の根を破棄して置き換える。親を持つ部品を渡すと例外を投げる。</summary>
    public static void SetContent(this WinUI.Window window, Node content) =>
        ウインドウの持ち主Map.GetValue(window, static ウインドウ => new WindowHost(ウインドウ)).SetRoot(content);
}
