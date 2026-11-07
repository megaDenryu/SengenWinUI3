using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>押したときの処理か、指定した URI を開く操作に使うリンク。</summary>
public sealed class HyperlinkButton : Node, IClickable, IControlNode
{
    private static readonly EventBinding<WinUI.Controls.HyperlinkButton, WinUI.RoutedEventHandler, EventArgs> 押された出来事 = new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(EventArgs.Empty),
        static (本物, ハンドラ) => 本物.Click += ハンドラ,
        static (本物, ハンドラ) => 本物.Click -= ハンドラ);

    private readonly WinUI.Controls.HyperlinkButton 本物 = new();
    private readonly ElementValue<WinUI.Controls.HyperlinkButton, string> 文字;

    internal HyperlinkButton(string content)
    {
        文字 = new(本物, static (本物, 値) => Equals(本物.Content, 値), static (本物, 値) => 本物.Content = 値);
        文字.Set(content);
        this.WireEvent("HyperlinkButton.AttachedFlyout", 本物, 押された出来事, 付けた画面を開く);
    }

    internal HyperlinkButton(string content, Uri navigateUri) : this(content) => 本物.NavigateUri = navigateUri;

    internal override WinUI.FrameworkElement Element => 本物;
    WinUI.Controls.Control IControlNode.Control => 本物;

    /// <summary>リンクに表示する文字を替える。</summary>
    public HyperlinkButton SetContent(string content)
    {
        ThrowIfDestroyed();
        文字.Set(content);
        return this;
    }

    /// <summary>押されたときの処理を登録する。2回目の登録は例外になる。</summary>
    public HyperlinkButton OnClick(Action handler) => this.WireEvent(nameof(OnClick), 本物, 押された出来事, handler);

    void IClickable.AttachFlyout(WinUI.Controls.Primitives.FlyoutBase flyout) => WinUI.Controls.Primitives.FlyoutBase.SetAttachedFlyout(本物, flyout);
    void IClickable.DetachFlyout() => WinUI.Controls.Primitives.FlyoutBase.SetAttachedFlyout(本物, null);
    internal override void ApplyStyle(Style style) => ControlStyle.All.Apply(style, 本物, this);
    private void 付けた画面を開く()
    {
        if (WinUI.Controls.Primitives.FlyoutBase.GetAttachedFlyout(本物) is { } 画面) 画面.ShowAt(本物);
    }
}
