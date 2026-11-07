using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>部品の近くに重ねて出す小さな画面。押せる部品へ <c>.Flyout(flyout)</c> で付け、押したら開く。作る関数は <see cref="UI.Flyout(Node)"/>。</summary>
public sealed class Flyout : FloatingNode
{
    private readonly Node 中身;
    private bool 中身を手放したか;

    internal Flyout(Node content)
    {
        中身 = content;
        content.SetOwner(this);
        Element.Content = content.Element;
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
        if (!中身を手放したか)
        {
            中身.Destroy();
        }
    }

    private protected override void ReleaseContent(Node content)
    {
        Element.Content = null;
        中身を手放したか = true;
    }
}
