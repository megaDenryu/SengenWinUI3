using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>キーの組み合わせ(ショートカットキー)の出来事。</summary>
public static class KeyboardAcceleratorExtensions
{
    /// <summary>
    /// <paramref name="key"/> と <paramref name="modifiers"/> の組み合わせが押されたときに処理を呼ぶ。部品の破棄で登録を解く。
    /// 同じ部品の同じ組み合わせへの2回目の登録は例外になる。
    /// </summary>
    public static T OnKeyboardAcceleratorInvoked<T>(this T node, Key key, VirtualKeyModifiers modifiers, Action<KeyboardAcceleratorInput> handler) where T : Node
    {
        node.ThrowIfDestroyed();
        Windows.System.VirtualKey 本物のキー = key.ToVirtualKey();
        Windows.System.VirtualKeyModifiers 本物の修飾キー = modifiers.ToWinUI();
        EventBinding<WinUI.FrameworkElement, Windows.Foundation.TypedEventHandler<WinUI.Input.KeyboardAccelerator, WinUI.Input.KeyboardAcceleratorInvokedEventArgs>, WinUI.Input.KeyboardAcceleratorInvokedEventArgs> 出来事 = new(
            static (本物, 出来事を渡す) => (送信元, 引数) => 出来事を渡す(引数),
            (本物, ハンドラ) =>
            {
                WinUI.Input.KeyboardAccelerator 組み合わせ = new() { Key = 本物のキー, Modifiers = 本物の修飾キー };
                組み合わせ.Invoked += ハンドラ;
                本物.KeyboardAccelerators.Add(組み合わせ);
                return new CallOnDispose(() => 本物.KeyboardAccelerators.Remove(組み合わせ));
            });
        return node.WireEvent($"{nameof(OnKeyboardAcceleratorInvoked)}:{key}:{modifiers}", 出来事, 引数 => handler(new KeyboardAcceleratorInput(引数)));
    }
}
