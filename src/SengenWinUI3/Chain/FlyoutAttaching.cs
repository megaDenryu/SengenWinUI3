using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// 本物が FlyoutBase の浮かぶ部品(Flyout・MenuFlyout)を部品に付ける手順。押したら開く付け方(Flyout チェーン)と右クリックで開く付け方(ContextFlyout チェーン)は、本物を置く先と印の名前だけが違う。
internal static class FlyoutAttaching
{
    // 同じ印の名前の2回目と、別の部品に付けた浮かぶ部品は例外にする。浮かぶ部品だけを破棄すると、本物を置く先から外し、印を戻す。
    internal static void AttachFlyoutTo<TFlyout>(this TFlyout 浮かぶ部品, Node node, string 印の名前,
        Action<WinUI.Controls.Primitives.FlyoutBase> 本物を置く, Action 本物を外す) where TFlyout : FloatingNode, IFlyoutNode
    {
        浮かぶ部品.ThrowIfAttached();
        node.MarkWiredOnce(印の名前);
        本物を置く(浮かぶ部品.FlyoutElement);
        浮かぶ部品.AttachTo(node, () =>
        {
            本物を外す();
            node.UnmarkWired(印の名前);
        });
    }
}
