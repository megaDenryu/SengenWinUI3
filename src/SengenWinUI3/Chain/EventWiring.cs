using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// 破棄済みの確認・1回の登録・ハンドラの保護・解除の所有を、すべての On〜 で共有する。
internal static class EventWiring
{
    internal static T WireEvent<T, TElement, THandler, TValue>(this T node, string eventName, TElement element, EventBinding<TElement, THandler, TValue> binding, Action<TValue> handler) where T : Node
    {
        node.MarkWiredOnce(eventName);
        IDisposable 登録 = binding.Subscribe(element, 値 =>
        {
            if (!node.IsDestroyed) handler(値);
        });
        node.OwnDisposable(登録);
        return node;
    }

    internal static T WireEvent<T, TElement, THandler>(this T node, string eventName, TElement element, EventBinding<TElement, THandler, EventArgs> binding, Action handler) where T : Node =>
        node.WireEvent(eventName, element, binding, _ => handler());

    internal static T WireEvent<T, THandler, TValue>(this T node, string eventName, EventBinding<WinUI.FrameworkElement, THandler, TValue> binding, Action<TValue> handler) where T : Node
    {
        node.ThrowIfDestroyed();
        return node.WireEvent(eventName, node.Element, binding, handler);
    }

    internal static T WireEvent<T, THandler>(this T node, string eventName, EventBinding<WinUI.FrameworkElement, THandler, EventArgs> binding, Action handler) where T : Node =>
        node.WireEvent(eventName, binding, _ => handler());
}
