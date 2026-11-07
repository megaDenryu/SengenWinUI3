using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>部品の近くに重ねて出す小さな画面。押せる部品へ <c>.Flyout(flyout)</c> で付け、押したら開く。作る関数は <see cref="UI.Flyout(Node)"/>。</summary>
public sealed class Flyout : FloatingNode
{
    private readonly ContentSlot 中身置き場;

    internal Flyout(Node content)
    {
        中身置き場 = new ContentSlot(this, 中身の本物 => Element.Content = 中身の本物);
        中身置き場.SetContent(content);
    }

    internal WinUI.Controls.Flyout Element { get; } = new();

    /// <summary>開いていれば閉じる。</summary>
    public void Hide()
    {
        ThrowIfDestroyed();
        Element.Hide();
    }

    internal override void OnDestroying()
    {
        Element.Hide();
        中身置き場.DestroyContent();
    }

    private protected override void ReleaseContent(Node content) => 中身置き場.Release(content);
}
