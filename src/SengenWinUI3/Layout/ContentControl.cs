using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>
/// 中身を1つ持つ Control。既定のテンプレートは中身を置くだけで、背景と角の丸みを描かない。フォーカスを受け、Tab キーで選べる。
/// 作る関数は <see cref="UI.ContentControl(Node, Style?)"/>。
/// </summary>
public sealed class ContentControl : Node, IControlNode, IContentOwner
{
    private readonly WinUI.Controls.ContentControl 本物 = new();
    private readonly ContentSlot 中身置き場;

    internal ContentControl(Node content)
    {
        中身置き場 = new ContentSlot(this, 中身の本物 => 本物.Content = 中身の本物);
        中身置き場.SetContent(content);
    }

    internal override WinUI.FrameworkElement Element => 本物;

    WinUI.Controls.Control IControlNode.Control => 本物;

    /// <summary>新しい中身を置き、前の中身を破棄する。親を持つ部品を渡すと例外になる。</summary>
    public ContentControl SetContent(Node content)
    {
        ThrowIfDestroyed();
        中身置き場.SetContent(content);
        return this;
    }

    void IContentOwner.RemoveChildElement(Node child) => 中身置き場.Release(child);

    internal override void ApplyStyle(Style style) => ControlStyle.All.Apply(style, 本物, this);
}
