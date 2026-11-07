using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>押せる部品(<see cref="IClickable"/>)に、押したら開く浮かぶ部品を付けるチェーン。</summary>
public static class FlyoutExtensions
{
    /// <summary>
    /// 押したら開く <see cref="SengenWinUI3.Flyout"/> を付ける。付けた Flyout は、この部品の破棄で一緒に破棄される。Flyout だけを破棄すると、この部品から外れ、別の Flyout を付けられる。
    /// 同じ部品へ2回付けたとき(MenuFlyout を付けた後を含む)と、別の部品に付けた Flyout を付けたときは例外を投げる。
    /// </summary>
    public static T Flyout<T>(this T node, Flyout flyout) where T : Node, IClickable =>
        押したら開くように付ける(node, flyout, flyout.Element);

    /// <summary>
    /// 押したら開く <see cref="SengenWinUI3.MenuFlyout"/> を付ける。寿命と例外の約束は Flyout を付けるときと同じである。
    /// 右クリックで開くメニューは <c>ContextFlyout</c> のチェーンで付ける。
    /// </summary>
    public static T Flyout<T>(this T node, MenuFlyout menu) where T : Node, IClickable =>
        押したら開くように付ける(node, menu, menu.Element);

    private static T 押したら開くように付ける<T>(T node, FloatingNode 浮かぶ部品, WinUI.Controls.Primitives.FlyoutBase 本物)
        where T : Node, IClickable
    {
        浮かぶ部品.ThrowIfAttached();
        node.MarkWiredOnce(nameof(Flyout));
        node.AttachFlyout(本物);
        浮かぶ部品.AttachTo(node, () =>
        {
            node.DetachFlyout();
            node.UnmarkWired(nameof(Flyout));
        });
        return node;
    }
}
