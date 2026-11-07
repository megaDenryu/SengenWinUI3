using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>押せるボタン。文字か、中身の部品を見せる。作る関数は <see cref="UI.Button(string, Style?)"/> と <see cref="UI.Button(Node, Style?)"/>。</summary>
public sealed class Button : Node, IClickable, IControlNode
{
    private readonly WinUI.Controls.Button 本物 = new();
    private readonly ContentSlot 中身置き場;

    internal Button(string text) : this() => 本物.Content = text;

    internal Button(Node content) : this() => 中身置き場.SetContent(content);

    private Button() => 中身置き場 = new ContentSlot(this, 中身の本物 => 本物.Content = 中身の本物);

    internal override WinUI.FrameworkElement Element => 本物;

    WinUI.Controls.Control IControlNode.Control => 本物;

    /// <summary>中身を部品へ入れ替える。前の中身の部品は破棄する。親を持つ部品を渡すと例外を投げる。</summary>
    public Button SetContent(Node content)
    {
        ThrowIfDestroyed();
        中身置き場.SetContent(content);
        return this;
    }

    /// <summary>押されたときに処理を呼ぶ。2回目は例外を投げる。</summary>
    public Button OnClick(Action handler)
    {
        WinUI.RoutedEventHandler 出来事が起きたら呼ぶ = (_, _) =>
        {
            if (!IsDestroyed) handler();
        };
        WireEvent(nameof(OnClick), () => 本物.Click += 出来事が起きたら呼ぶ, () => 本物.Click -= 出来事が起きたら呼ぶ);
        return this;
    }

    void IClickable.AttachFlyout(Flyout flyout) => 本物.Flyout = flyout.Element;

    void IClickable.DetachFlyout() => 本物.Flyout = null;

    internal override void ApplyStyle(Style style) => ControlStyle.All.Apply(style, 本物, this);

    internal override void RemoveChildElement(Node child) => 中身置き場.Release(child);
}
