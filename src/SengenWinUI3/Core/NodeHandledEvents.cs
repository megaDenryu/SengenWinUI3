using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// 子の部品が処理済みにした出来事も受ける購読の定義。WinUI の AddHandler の handledEventsToo を true にして付ける。
// ボタンのような子の部品は PointerPressed を処理済みにするので、ふつうの購読では親に届かない。
internal static class NodeHandledEvents
{
    internal static readonly EventBinding<WinUI.FrameworkElement, WinUI.Input.PointerEventHandler, WinUI.Input.PointerRoutedEventArgs> PointerPressed =
        ポインターの出来事(WinUI.UIElement.PointerPressedEvent);

    internal static readonly EventBinding<WinUI.FrameworkElement, WinUI.Input.PointerEventHandler, WinUI.Input.PointerRoutedEventArgs> PointerReleased =
        ポインターの出来事(WinUI.UIElement.PointerReleasedEvent);

    internal static readonly EventBinding<WinUI.FrameworkElement, WinUI.Input.PointerEventHandler, WinUI.Input.PointerRoutedEventArgs> PointerCanceled =
        ポインターの出来事(WinUI.UIElement.PointerCanceledEvent);

    internal static readonly EventBinding<WinUI.FrameworkElement, WinUI.Input.PointerEventHandler, WinUI.Input.PointerRoutedEventArgs> PointerCaptureLost =
        ポインターの出来事(WinUI.UIElement.PointerCaptureLostEvent);

    internal static readonly EventBinding<WinUI.FrameworkElement, WinUI.Input.KeyEventHandler, WinUI.Input.KeyRoutedEventArgs> KeyUp = new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(出来事),
        static (本物, ハンドラ) => 処理済みも受けて購読する(本物, WinUI.UIElement.KeyUpEvent, ハンドラ));

    private static EventBinding<WinUI.FrameworkElement, WinUI.Input.PointerEventHandler, WinUI.Input.PointerRoutedEventArgs> ポインターの出来事(WinUI.RoutedEvent 経路つきの出来事) => new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(出来事),
        (本物, ハンドラ) => 処理済みも受けて購読する(本物, 経路つきの出来事, ハンドラ));

    private static CallOnDispose 処理済みも受けて購読する(WinUI.FrameworkElement 本物, WinUI.RoutedEvent 経路つきの出来事, Delegate ハンドラ)
    {
        本物.AddHandler(経路つきの出来事, ハンドラ, true);
        return new CallOnDispose(() => 本物.RemoveHandler(経路つきの出来事, ハンドラ));
    }
}
