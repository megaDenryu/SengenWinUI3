using System.Runtime.CompilerServices;
using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>アプリの Window に根の部品を置く拡張メソッド。</summary>
public static class WindowExtensions
{
    // Window へフィールドを足せないので、根の寿命を管理する WindowHost をこの表に持つ。ウインドウが回収されれば表からも消える。
    private static readonly ConditionalWeakTable<WinUI.Window, WindowHost> ウインドウの持ち主Map = new();

    /// <summary>根の部品をウインドウに置く。ウインドウが閉じたら根を破棄する。2回目は前の根を破棄して置き換える。親を持つ部品を渡すと例外を投げる。</summary>
    public static void SetContent(this WinUI.Window window, Node content) => 持ち主を得る(window).SetRoot(content);

    private static WindowHost 持ち主を得る(WinUI.Window window) =>
        ウインドウの持ち主Map.GetValue(window, static ウインドウ => new WindowHost(ウインドウ));
}
