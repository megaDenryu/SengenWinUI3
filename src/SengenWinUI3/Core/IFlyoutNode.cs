using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// 本物が WinUI の FlyoutBase である浮かぶ部品(Flyout・MenuFlyout)の目印。付けるチェーンは、浮かぶ部品とその本物を別々に受けず、この目印から本物を取る。
internal interface IFlyoutNode
{
    WinUI.Controls.Primitives.FlyoutBase FlyoutElement { get; }
}
