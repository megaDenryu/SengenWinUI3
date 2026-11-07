namespace SengenWinUI3;

/// <summary>押せる部品の目印。押したら開く <see cref="Flyout"/> を付けられる。SengenWinUI3 の外からは実装できない。</summary>
public interface IClickable
{
    internal void AttachFlyout(Flyout flyout);

    internal void DetachFlyout();
}
