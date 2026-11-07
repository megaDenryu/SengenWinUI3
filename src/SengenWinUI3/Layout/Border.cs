using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>中身を1つ持つ枠。枠線・背景・内側の余白は Style で指定する。</summary>
public sealed class Border : Node, IContentOwner
{
    private readonly WinUI.Controls.Border 本物 = new();
    private readonly ContentSlot 中身置き場;

    internal Border(Node content)
    {
        中身置き場 = new ContentSlot(this, 中身の本物 => 本物.Child = 中身の本物);
        中身置き場.SetContent(content);
    }

    internal override WinUI.FrameworkElement Element => 本物;

    /// <summary>新しい中身を置き、前の中身を破棄する。親を持つ部品を渡すと例外になる。</summary>
    public Border SetContent(Node content)
    {
        ThrowIfDestroyed();
        中身置き場.SetContent(content);
        return this;
    }

    void IContentOwner.RemoveChildElement(Node child) => 中身置き場.Release(child);

    internal override void ApplyStyle(Style style) => BorderStyle.All.Apply(style, 本物, this);
}
