using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>全部の部品に共通の出来事のチェーン。同じ部品に同じ出来事を2回受けると例外を投げる。</summary>
public static class NodeEventExtensions
{
    /// <summary>部品の大きさが変わったときに処理を呼ぶ。</summary>
    public static T OnSizeChanged<T>(this T node, Action<ViewSize> handler) where T : Node
    {
        node.MarkEventWired(NodeEvents.SizeChanged);
        node.Element.SizeChanged += (_, 出来事) => handler(new ViewSize(new Px(出来事.NewSize.Width), new Px(出来事.NewSize.Height)));
        return node;
    }

    /// <summary>部品かその中の部品にフォーカスがある間に、キーが押されたときに処理を呼ぶ。</summary>
    public static T OnKeyDown<T>(this T node, Action<KeyInput> handler) where T : Node
    {
        node.MarkEventWired(NodeEvents.KeyDown);
        node.Element.KeyDown += (_, 出来事) => handler(new KeyInput(出来事));
        return node;
    }

    /// <summary>ポインターが部品の上に入ったときに処理を呼ぶ。</summary>
    public static T OnPointerEntered<T>(this T node, Action<PointerInput> handler) where T : Node =>
        ポインターの出来事を受ける(node, NodeEvents.PointerEntered, handler, static (本物, 受け手) => 本物.PointerEntered += 受け手);

    /// <summary>ポインターが部品の上から出たときに処理を呼ぶ。</summary>
    public static T OnPointerExited<T>(this T node, Action<PointerInput> handler) where T : Node =>
        ポインターの出来事を受ける(node, NodeEvents.PointerExited, handler, static (本物, 受け手) => 本物.PointerExited += 受け手);

    /// <summary>部品の上でボタンが押されたか、指やペンが触れたときに処理を呼ぶ。</summary>
    public static T OnPointerPressed<T>(this T node, Action<PointerInput> handler) where T : Node =>
        ポインターの出来事を受ける(node, NodeEvents.PointerPressed, handler, static (本物, 受け手) => 本物.PointerPressed += 受け手);

    /// <summary>部品の上でボタンが離されたか、指やペンが離れたときに処理を呼ぶ。</summary>
    public static T OnPointerReleased<T>(this T node, Action<PointerInput> handler) where T : Node =>
        ポインターの出来事を受ける(node, NodeEvents.PointerReleased, handler, static (本物, 受け手) => 本物.PointerReleased += 受け手);

    private static T ポインターの出来事を受ける<T>(T node, NodeEvents 出来事, Action<PointerInput> handler, Action<WinUI.UIElement, WinUI.Input.PointerEventHandler> 受け手を足す) where T : Node
    {
        node.MarkEventWired(出来事);
        WinUI.FrameworkElement 本物 = node.Element;
        受け手を足す(本物, (_, 元の出来事) => handler(new PointerInput(元の出来事, 本物)));
        return node;
    }
}
