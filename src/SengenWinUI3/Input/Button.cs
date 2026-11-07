using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>押せるボタン。文字か、中身の部品を見せる。作る関数は <see cref="UI.Button(string, Style?)"/> と <see cref="UI.Button(Node, Style?)"/>。</summary>
public sealed class Button : Node, IClickable, IControlNode
{
    private readonly WinUI.Controls.Button 本物 = new();
    private bool 押したときを受けたか;

    internal Button(string text) => 本物.Content = text;

    internal Button(Node content)
    {
        AdoptChild(content);
        本物.Content = content.Element;
    }

    internal override WinUI.FrameworkElement Element => 本物;

    WinUI.Controls.Control IControlNode.Control => 本物;

    /// <summary>押されたときに処理を呼ぶ。2回目は例外を投げる。</summary>
    public Button OnClick(Action handler)
    {
        ThrowIfWiredTwice(ref 押したときを受けたか, nameof(OnClick));
        本物.Click += (_, _) => handler();
        return this;
    }

    void IClickable.AttachFlyout(Flyout flyout) => 本物.Flyout = flyout.Element;

    internal override void ApplyStyle(Style style)
    {
        style.ThrowIfUnsupported(StyleItems.Layout | StyleItems.ControlLook, this);
        style.ApplyLayout(本物);
        style.ApplyControlLook(本物);
    }

    internal override void RemoveChildElement(Node child) => 本物.Content = null;
}
