using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>中身を1つ持ち、自分の大きさに合わせて縦横比を保って拡大縮小する枠。大きさは Style の Width・Height で指定する。</summary>
public sealed class Viewbox : Node, IContentOwner
{
    private static readonly StyleTarget<WinUI.Controls.Viewbox> 当て方 = new(FrameworkElementStyle.Layout);

    private readonly WinUI.Controls.Viewbox 本物 = new();
    private readonly ContentSlot 中身置き場;

    internal Viewbox(Node content)
    {
        中身置き場 = new ContentSlot(this, 中身の本物 => 本物.Child = 中身の本物);
        中身置き場.SetContent(content);
    }

    internal override WinUI.FrameworkElement Element => 本物;

    /// <summary>新しい中身を置き、前の中身を破棄する。親を持つ部品を渡すと例外になる。</summary>
    public Viewbox SetContent(Node content)
    {
        ThrowIfDestroyed();
        中身置き場.SetContent(content);
        return this;
    }

    void IContentOwner.RemoveChildElement(Node child) => 中身置き場.Release(child);

    internal override void ApplyStyle(Style style) => 当て方.Apply(style, 本物, this);
}
