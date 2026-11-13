using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// ドラッグ&ドロップの購読の定義。引数の変換は EventWiring の保護の内側で行う。
internal static class NodeDragEvents
{
    internal static readonly EventBinding<WinUI.FrameworkElement, WinUI.DragEventHandler, WinUI.DragEventArgs> DragOver = new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(出来事),
        static (本物, ハンドラ) => 本物.DragOver += ハンドラ,
        static (本物, ハンドラ) => 本物.DragOver -= ハンドラ);

    internal static readonly EventBinding<WinUI.FrameworkElement, WinUI.DragEventHandler, WinUI.DragEventArgs> Drop = new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(出来事),
        static (本物, ハンドラ) => 本物.Drop += ハンドラ,
        static (本物, ハンドラ) => 本物.Drop -= ハンドラ);
}
