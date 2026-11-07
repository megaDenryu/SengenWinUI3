namespace SengenWinUI3;

/// <summary>右クリックなどで開くメニューを部品に付ける。</summary>
public static class MenuFlyoutExtensions
{
    /// <summary>メニューを部品の寿命に結び付ける。メニューだけの破棄で外れ、再び付けられる。</summary>
    public static T ContextFlyout<T>(this T node, MenuFlyout menu) where T : Node
    {
        menu.AttachFlyoutTo(node, nameof(ContextFlyout), 本物 => node.Element.ContextFlyout = 本物, () => node.Element.ContextFlyout = null);
        return node;
    }
}
