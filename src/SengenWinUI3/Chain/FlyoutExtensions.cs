namespace SengenWinUI3;

/// <summary>押せる部品(<see cref="IClickable"/>)に浮かぶ部品を付けるチェーン。</summary>
public static class FlyoutExtensions
{
    /// <summary>押したら開く <see cref="SengenWinUI3.Flyout"/> を付ける。付けた Flyout は、この部品の破棄で一緒に破棄される。</summary>
    public static T Flyout<T>(this T node, Flyout flyout) where T : Node, IClickable
    {
        node.ThrowIfDestroyed();
        flyout.AttachTo(node);
        node.AttachFlyout(flyout);
        return node;
    }
}
