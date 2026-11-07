using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>文字の入力欄。複数行かどうかは AcceptsReturn と TextWrapping で指定する。</summary>
public sealed class TextBox : Node, IControlNode
{
    private static readonly EventBinding<WinUI.Controls.TextBox, WinUI.Controls.TextChangedEventHandler, EventArgs> 変更の出来事 = new(
        static (本物, 呼ぶ) => (送信元, 出来事) => 呼ぶ(EventArgs.Empty),
        static (本物, ハンドラ) => 本物.TextChanged += ハンドラ,
        static (本物, ハンドラ) => 本物.TextChanged -= ハンドラ);

    private readonly WinUI.Controls.TextBox 本物 = new();
    private readonly InputValue<string> 文字;

    internal TextBox(string text, string placeholderText, bool acceptsReturn, TextWrapping textWrapping)
    {
        本物.PlaceholderText = placeholderText;
        本物.AcceptsReturn = acceptsReturn;
        本物.TextWrapping = textWrapping.ToWinUI();
        ElementValue<WinUI.Controls.TextBox, string> 設定先 = new(本物,
            static (本物, 値) => (本物.Text).Equals(値),
            static (本物, 値) => 本物.Text = 値);
        文字 = new(text, 設定先.Set);
        文字.Set(text);
    }

    internal override WinUI.FrameworkElement Element => 本物;
    WinUI.Controls.Control IControlNode.Control => 本物;

    /// <summary>プログラムから値を設定する。この操作では変更を通知しない。</summary>
    public TextBox SetText(string text)
    {
        ThrowIfDestroyed();
        文字.Set(text);
        return this;
    }

    /// <summary>利用者の変更を通知する。最後の設定値へ戻した場合は通知しない。2回目の登録は例外になる。</summary>
    public TextBox OnTextChanged(Action<string> handler) =>
        this.WireEvent(nameof(OnTextChanged), 本物, 変更の出来事, () => 利用者の変更を知らせる(handler));

    internal override void ApplyStyle(Style style) => ControlStyle.All.Apply(style, 本物, this);

    private void 利用者の変更を知らせる(Action<string> 呼ぶ)
    {
        string 現在値 = 本物.Text;
        if (文字.IsUserChange(現在値)) 呼ぶ(現在値);
    }
}
