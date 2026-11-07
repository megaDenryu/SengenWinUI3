using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>文字の入力欄。複数行かどうかは AcceptsReturn と TextWrapping で指定する。</summary>
public sealed class TextBox : Node, IControlNode
{
    private static readonly EventBinding<WinUI.Controls.TextBox, WinUI.Controls.TextChangedEventHandler, EventArgs> 変更の出来事 = new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(EventArgs.Empty),
        static (本物, ハンドラ) => 本物.TextChanged += ハンドラ,
        static (本物, ハンドラ) => 本物.TextChanged -= ハンドラ);

    private readonly WinUI.Controls.TextBox 本物 = new();
    private readonly InputValue<string> 文字;

    internal TextBox(string text, string placeholderText, bool acceptsReturn, TextWrapping textWrapping)
    {
        本物.PlaceholderText = placeholderText;
        本物.AcceptsReturn = acceptsReturn;
        本物.TextWrapping = textWrapping.ToWinUI();
        文字 = InputValue.Create(本物, text,
            static (本物, 値) => string.Equals(本物.Text, 値, StringComparison.Ordinal),
            static (本物, 値) => 本物.Text = 値);
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

    /// <summary>ふだん・重ねた・フォーカス・押せないの状態ごとの色を設定する。木へ入れる前に呼ぶ。読み込まれた後に呼ぶと例外を投げる。</summary>
    public TextBox SetColors(TextBoxColors colors)
    {
        ThrowIfDestroyed();
        LightweightResources.ThrowIfLoaded(本物, this);
        colors.WriteTo(本物);
        return this;
    }

    /// <summary>利用者の変更を通知する。最後の設定値へ戻した場合は通知しない。2回目の登録は例外になる。</summary>
    public TextBox OnTextChanged(Action<string> handler) =>
        this.WireEvent(nameof(OnTextChanged), 本物, 変更の出来事, () => 利用者の変更を知らせる(handler));

    internal override void ApplyStyle(Style style) => ControlStyle.All.Apply(style, 本物, this);

    private void 利用者の変更を知らせる(Action<string> 呼ぶ)
    {
        string 今の文字 = 本物.Text;
        if (文字.IsUserChange(今の文字)) 呼ぶ(今の文字);
    }
}
