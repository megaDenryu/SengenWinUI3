namespace SengenWinUI3;

/// <summary>押せる部品(<see cref="IClickable"/>)に浮かぶ部品を付けるチェーン。</summary>
public static class FlyoutExtensions
{
    /// <summary>
    /// 押したら開く <see cref="SengenWinUI3.Flyout"/> を付ける。付けた Flyout は、この部品の破棄で一緒に破棄される。Flyout だけを破棄すると、この部品から外れ、別の Flyout を付けられる。
    /// 同じ部品へ2回付けたときと、別の部品に付けた Flyout を付けたときは例外を投げる。
    /// </summary>
    public static T Flyout<T>(this T node, Flyout flyout) where T : Node, IClickable
    {
        flyout.ThrowIfAttached();
        node.MarkWiredOnce(nameof(Flyout));
        node.AttachFlyout(flyout);
        flyout.AttachTo(node, () =>
        {
            node.DetachFlyout();
            node.UnmarkWired(nameof(Flyout));
        });
        return node;
    }
}
