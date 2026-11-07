namespace SengenWinUI3;

/// <summary>全部の部品の出来事。同じ出来事への2回目の登録は例外になり、部品の破棄で登録を解く。</summary>
public static class NodeEventExtensions
{
    /// <summary>部品の大きさが変わったときに処理を呼ぶ。</summary>
    public static T OnSizeChanged<T>(this T node, Action<ViewSize> handler) where T : Node =>
        node.WireEvent(nameof(OnSizeChanged), NodeEvents.SizeChanged, 出来事 => handler(new ViewSize(new Px(出来事.NewSize.Width), new Px(出来事.NewSize.Height))));

    /// <summary>部品かその中にフォーカスがある間にキーが押されたときに処理を呼ぶ。</summary>
    public static T OnKeyDown<T>(this T node, Action<KeyInput> handler) where T : Node =>
        node.WireEvent(nameof(OnKeyDown), NodeEvents.KeyDown, 出来事 => handler(new KeyInput(出来事)));

    /// <summary>ポインターが部品の上に入ったときに処理を呼ぶ。</summary>
    public static T OnPointerEntered<T>(this T node, Action<PointerInput> handler) where T : Node =>
        node.WireEvent(nameof(OnPointerEntered), NodeEvents.PointerEntered, 出来事 => handler(new PointerInput(出来事, node.Element)));

    /// <summary>ポインターが部品の上から出たときに処理を呼ぶ。</summary>
    public static T OnPointerExited<T>(this T node, Action<PointerInput> handler) where T : Node =>
        node.WireEvent(nameof(OnPointerExited), NodeEvents.PointerExited, 出来事 => handler(new PointerInput(出来事, node.Element)));

    /// <summary>部品の上でボタンが押されたか、指やペンが触れたときに処理を呼ぶ。</summary>
    public static T OnPointerPressed<T>(this T node, Action<PointerInput> handler) where T : Node =>
        node.WireEvent(nameof(OnPointerPressed), NodeEvents.PointerPressed, 出来事 => handler(new PointerInput(出来事, node.Element)));

    /// <summary>部品の上でボタンが離されたか、指やペンが離れたときに処理を呼ぶ。</summary>
    public static T OnPointerReleased<T>(this T node, Action<PointerInput> handler) where T : Node =>
        node.WireEvent(nameof(OnPointerReleased), NodeEvents.PointerReleased, 出来事 => handler(new PointerInput(出来事, node.Element)));
}
