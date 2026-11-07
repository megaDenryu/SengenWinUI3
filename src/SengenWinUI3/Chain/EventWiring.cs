using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// 破棄済みの確認・1回の登録・ハンドラの保護・解除の所有を、部品と浮かぶ部品のすべての出来事の購読で共有する。
internal static class EventWiring
{
    // On〜 の形。同じ名前の2回目の登録を例外にし、解除を持ち主の破棄へ預ける。
    internal static T WireEvent<T, TElement, THandler, TValue>(this T owner, string eventName, TElement element, EventBinding<TElement, THandler, TValue> binding, Action<TValue> handler) where T : ILifetimeOwner
    {
        owner.Lifetime.MarkWiredOnce(eventName);
        owner.Lifetime.OwnDisposable(破棄の後は呼ばないように購読する(owner.Lifetime, element, binding, handler));
        return owner;
    }

    internal static T WireEvent<T, TElement, THandler>(this T owner, string eventName, TElement element, EventBinding<TElement, THandler, EventArgs> binding, Action handler) where T : ILifetimeOwner =>
        owner.WireEvent(eventName, element, binding, _ => handler());

    internal static T WireEvent<T, THandler, TValue>(this T node, string eventName, EventBinding<WinUI.FrameworkElement, THandler, TValue> binding, Action<TValue> handler) where T : Node
    {
        node.ThrowIfDestroyed();
        return node.WireEvent(eventName, node.Element, binding, handler);
    }

    internal static T WireEvent<T, THandler>(this T node, string eventName, EventBinding<WinUI.FrameworkElement, THandler, EventArgs> binding, Action handler) where T : Node =>
        node.WireEvent(eventName, binding, _ => handler());

    // 持ち主が自分で作った本物(MenuFlyout の項目など)の出来事。本物ごとに1回だけ作るので名前の印を付けず、解除を持ち主の破棄へ預ける。
    internal static void WireEventOfOwnElement<TElement, THandler>(this ILifetimeOwner owner, TElement element, EventBinding<TElement, THandler, EventArgs> binding, Action handler) =>
        owner.Lifetime.OwnDisposable(破棄の後は呼ばないように購読する(owner.Lifetime, element, binding, _ => handler()));

    // 途中で解く購読(Popup の読み込み待ちなど)。返した値を Dispose すると先に解け、Dispose しなくても持ち主の破棄で解ける。
    internal static IDisposable WireRemovableEvent<TElement, THandler>(this ILifetimeOwner owner, TElement element, EventBinding<TElement, THandler, EventArgs> binding, Action handler) =>
        owner.Lifetime.OwnRemovable(破棄の後は呼ばないように購読する(owner.Lifetime, element, binding, _ => handler()));

    private static IDisposable 破棄の後は呼ばないように購読する<TElement, THandler, TValue>(Lifetime 寿命, TElement element, EventBinding<TElement, THandler, TValue> binding, Action<TValue> handler) =>
        binding.Subscribe(element, 値 =>
        {
            if (!寿命.IsDestroyed) handler(値);
        });
}
