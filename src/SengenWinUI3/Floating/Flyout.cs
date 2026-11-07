using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>部品の近くに重ねて出す小さな画面。押せる部品へ <c>.Flyout(flyout)</c> で付け、押したら開く。作る関数は <see cref="UI.Flyout(Node)"/>。</summary>
public sealed class Flyout : FloatingNode, IFlyoutNode
{
    private readonly WinUI.Controls.Flyout 本物 = new();
    private readonly FloatingContent 中身置き場;

    internal Flyout(Node content)
    {
        中身置き場 = new(this, 中身の本物 => 本物.Content = 中身の本物);
        中身置き場.SetContent(content);
    }

    WinUI.Controls.Primitives.FlyoutBase IFlyoutNode.FlyoutElement => 本物;

    /// <summary>開いていれば閉じる。</summary>
    public void Hide()
    {
        ThrowIfDestroyed();
        本物.Hide();
    }

    internal override void OnDestroying()
    {
        CollectedExceptions 集めた例外 = new();
        集めた例外.Run(本物.Hide);
        集めた例外.Run(中身置き場.DestroyContent);
        集めた例外.ThrowIfAny();
    }
}
