using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// 本物の出来事の型と購読・解除を定義する。引数の変換は EventWiring の保護の内側で行う。
internal static class NodeEvents
{
    internal static readonly EventBinding<WinUI.FrameworkElement, WinUI.RoutedEventHandler, EventArgs> Loaded = new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(EventArgs.Empty),
        static (本物, ハンドラ) => 本物.Loaded += ハンドラ,
        static (本物, ハンドラ) => 本物.Loaded -= ハンドラ);

    internal static readonly EventBinding<WinUI.FrameworkElement, WinUI.RoutedEventHandler, EventArgs> Unloaded = new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(EventArgs.Empty),
        static (本物, ハンドラ) => 本物.Unloaded += ハンドラ,
        static (本物, ハンドラ) => 本物.Unloaded -= ハンドラ);

    internal static readonly EventBinding<WinUI.FrameworkElement, WinUI.SizeChangedEventHandler, WinUI.SizeChangedEventArgs> SizeChanged = new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(出来事),
        static (本物, ハンドラ) => 本物.SizeChanged += ハンドラ,
        static (本物, ハンドラ) => 本物.SizeChanged -= ハンドラ);

    internal static readonly EventBinding<WinUI.FrameworkElement, WinUI.Input.KeyEventHandler, WinUI.Input.KeyRoutedEventArgs> KeyDown = new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(出来事),
        static (本物, ハンドラ) => 本物.KeyDown += ハンドラ,
        static (本物, ハンドラ) => 本物.KeyDown -= ハンドラ);

    internal static readonly EventBinding<WinUI.FrameworkElement, WinUI.Input.KeyEventHandler, WinUI.Input.KeyRoutedEventArgs> PreviewKeyDown = new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(出来事),
        static (本物, ハンドラ) => 本物.PreviewKeyDown += ハンドラ,
        static (本物, ハンドラ) => 本物.PreviewKeyDown -= ハンドラ);

    internal static readonly EventBinding<WinUI.FrameworkElement, WinUI.Input.PointerEventHandler, WinUI.Input.PointerRoutedEventArgs> PointerEntered = new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(出来事),
        static (本物, ハンドラ) => 本物.PointerEntered += ハンドラ,
        static (本物, ハンドラ) => 本物.PointerEntered -= ハンドラ);

    internal static readonly EventBinding<WinUI.FrameworkElement, WinUI.Input.PointerEventHandler, WinUI.Input.PointerRoutedEventArgs> PointerExited = new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(出来事),
        static (本物, ハンドラ) => 本物.PointerExited += ハンドラ,
        static (本物, ハンドラ) => 本物.PointerExited -= ハンドラ);

    internal static readonly EventBinding<WinUI.FrameworkElement, WinUI.Input.PointerEventHandler, WinUI.Input.PointerRoutedEventArgs> PointerPressed = new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(出来事),
        static (本物, ハンドラ) => 本物.PointerPressed += ハンドラ,
        static (本物, ハンドラ) => 本物.PointerPressed -= ハンドラ);

    internal static readonly EventBinding<WinUI.FrameworkElement, WinUI.Input.PointerEventHandler, WinUI.Input.PointerRoutedEventArgs> PointerReleased = new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(出来事),
        static (本物, ハンドラ) => 本物.PointerReleased += ハンドラ,
        static (本物, ハンドラ) => 本物.PointerReleased -= ハンドラ);

    internal static readonly EventBinding<WinUI.FrameworkElement, WinUI.Input.PointerEventHandler, WinUI.Input.PointerRoutedEventArgs> PointerMoved = new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(出来事),
        static (本物, ハンドラ) => 本物.PointerMoved += ハンドラ,
        static (本物, ハンドラ) => 本物.PointerMoved -= ハンドラ);

    internal static readonly EventBinding<WinUI.FrameworkElement, WinUI.RoutedEventHandler, EventArgs> GotFocus = new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(EventArgs.Empty),
        static (本物, ハンドラ) => 本物.GotFocus += ハンドラ,
        static (本物, ハンドラ) => 本物.GotFocus -= ハンドラ);

    internal static readonly EventBinding<WinUI.FrameworkElement, WinUI.RoutedEventHandler, EventArgs> LostFocus = new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(EventArgs.Empty),
        static (本物, ハンドラ) => 本物.LostFocus += ハンドラ,
        static (本物, ハンドラ) => 本物.LostFocus -= ハンドラ);
}
