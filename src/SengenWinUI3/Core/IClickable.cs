using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>押せる部品の目印。押したら開く <see cref="Flyout"/> か <see cref="MenuFlyout"/> を付けられる。SengenWinUI3 の外からは実装できない。</summary>
public interface IClickable
{
    internal void AttachFlyout(WinUI.Controls.Primitives.FlyoutBase flyout);

    internal void DetachFlyout();
}
