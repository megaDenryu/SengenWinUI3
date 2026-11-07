using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>全部の部品に共通の出来事のチェーン。同じ部品に同じ出来事を2回受けると例外を投げる。購読は部品の破棄で解く。</summary>
public static class NodeEventExtensions
{
    /// <summary>部品の大きさが変わったときに処理を呼ぶ。</summary>
    public static T OnSizeChanged<T>(this T node, Action<ViewSize> handler) where T : Node
    {
        node.ThrowIfDestroyed();
        WinUI.FrameworkElement 本物 = node.Element;
        WinUI.SizeChangedEventHandler 出来事が起きたら呼ぶ = (_, 出来事) =>
        {
            if (!node.IsDestroyed) handler(new ViewSize(new Px(出来事.NewSize.Width), new Px(出来事.NewSize.Height)));
        };
        node.WireEvent(nameof(OnSizeChanged), () => 本物.SizeChanged += 出来事が起きたら呼ぶ, () => 本物.SizeChanged -= 出来事が起きたら呼ぶ);
        return node;
    }

    /// <summary>部品かその中の部品にフォーカスがある間に、キーが押されたときに処理を呼ぶ。</summary>
    public static T OnKeyDown<T>(this T node, Action<KeyInput> handler) where T : Node
    {
        node.ThrowIfDestroyed();
        WinUI.FrameworkElement 本物 = node.Element;
        WinUI.Input.KeyEventHandler 出来事が起きたら呼ぶ = (_, 出来事) =>
        {
            if (!node.IsDestroyed) handler(new KeyInput(出来事));
        };
        node.WireEvent(nameof(OnKeyDown), () => 本物.KeyDown += 出来事が起きたら呼ぶ, () => 本物.KeyDown -= 出来事が起きたら呼ぶ);
        return node;
    }

    /// <summary>ポインターが部品の上に入ったときに処理を呼ぶ。</summary>
    public static T OnPointerEntered<T>(this T node, Action<PointerInput> handler) where T : Node =>
        ポインターの出来事を受ける(node, nameof(OnPointerEntered), handler, static (本物, 出来事が起きたら呼ぶ) => 本物.PointerEntered += 出来事が起きたら呼ぶ, static (本物, 出来事が起きたら呼ぶ) => 本物.PointerEntered -= 出来事が起きたら呼ぶ);

    /// <summary>ポインターが部品の上から出たときに処理を呼ぶ。</summary>
    public static T OnPointerExited<T>(this T node, Action<PointerInput> handler) where T : Node =>
        ポインターの出来事を受ける(node, nameof(OnPointerExited), handler, static (本物, 出来事が起きたら呼ぶ) => 本物.PointerExited += 出来事が起きたら呼ぶ, static (本物, 出来事が起きたら呼ぶ) => 本物.PointerExited -= 出来事が起きたら呼ぶ);

    /// <summary>部品の上でボタンが押されたか、指やペンが触れたときに処理を呼ぶ。</summary>
    public static T OnPointerPressed<T>(this T node, Action<PointerInput> handler) where T : Node =>
        ポインターの出来事を受ける(node, nameof(OnPointerPressed), handler, static (本物, 出来事が起きたら呼ぶ) => 本物.PointerPressed += 出来事が起きたら呼ぶ, static (本物, 出来事が起きたら呼ぶ) => 本物.PointerPressed -= 出来事が起きたら呼ぶ);

    /// <summary>部品の上でボタンが離されたか、指やペンが離れたときに処理を呼ぶ。</summary>
    public static T OnPointerReleased<T>(this T node, Action<PointerInput> handler) where T : Node =>
        ポインターの出来事を受ける(node, nameof(OnPointerReleased), handler, static (本物, 出来事が起きたら呼ぶ) => 本物.PointerReleased += 出来事が起きたら呼ぶ, static (本物, 出来事が起きたら呼ぶ) => 本物.PointerReleased -= 出来事が起きたら呼ぶ);

    private static T ポインターの出来事を受ける<T>(
        T node,
        string 出来事の名前,
        Action<PointerInput> handler,
        Action<WinUI.UIElement, WinUI.Input.PointerEventHandler> 購読する,
        Action<WinUI.UIElement, WinUI.Input.PointerEventHandler> 購読を解く) where T : Node
    {
        node.ThrowIfDestroyed();
        WinUI.FrameworkElement 本物 = node.Element;
        WinUI.Input.PointerEventHandler 出来事が起きたら呼ぶ = (_, 出来事) =>
        {
            if (!node.IsDestroyed) handler(new PointerInput(出来事, 本物));
        };
        node.WireEvent(出来事の名前, () => 購読する(本物, 出来事が起きたら呼ぶ), () => 購読を解く(本物, 出来事が起きたら呼ぶ));
        return node;
    }
}
