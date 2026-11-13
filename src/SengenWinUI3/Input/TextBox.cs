using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>文字の入力欄。複数行かどうかは AcceptsReturn と TextWrapping で指定する。</summary>
public sealed class TextBox : Node, IControlNode
{
    private static readonly EventBinding<WinUI.Controls.TextBox, WinUI.Controls.TextChangedEventHandler, EventArgs> 変更の出来事 = new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(EventArgs.Empty),
        static (本物, ハンドラ) => 本物.TextChanged += ハンドラ,
        static (本物, ハンドラ) => 本物.TextChanged -= ハンドラ);

    private static readonly EventBinding<WinUI.Controls.TextBox, Windows.Foundation.TypedEventHandler<WinUI.Controls.TextBox, WinUI.Controls.TextCompositionStartedEventArgs>, EventArgs> 変換の始まりの出来事 = new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(EventArgs.Empty),
        static (本物, ハンドラ) => 本物.TextCompositionStarted += ハンドラ,
        static (本物, ハンドラ) => 本物.TextCompositionStarted -= ハンドラ);

    private static readonly EventBinding<WinUI.Controls.TextBox, Windows.Foundation.TypedEventHandler<WinUI.Controls.TextBox, WinUI.Controls.TextCompositionEndedEventArgs>, EventArgs> 変換の終わりの出来事 = new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(EventArgs.Empty),
        static (本物, ハンドラ) => 本物.TextCompositionEnded += ハンドラ,
        static (本物, ハンドラ) => 本物.TextCompositionEnded -= ハンドラ);

    private readonly WinUI.Controls.TextBox 本物 = new();
    private readonly InputValue<string> 文字;
    private bool 変換中;

    internal TextBox(string text, string placeholderText, bool acceptsReturn, TextWrapping textWrapping)
    {
        本物.PlaceholderText = placeholderText;
        本物.AcceptsReturn = acceptsReturn;
        本物.TextWrapping = textWrapping.ToWinUI();
        文字 = InputValue.Create(本物, text,
            static (本物, 値) => string.Equals(本物.Text, 値, StringComparison.Ordinal),
            static (本物, 値) => 本物.Text = 値);
        this.WireEventOfOwnElement(本物, 変換の始まりの出来事, () => 変換中 = true);
        this.WireEventOfOwnElement(本物, 変換の終わりの出来事, () => 変換中 = false);
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

    /// <summary>今の文字を返す。<see cref="OnTextChanged"/> は最後の <see cref="SetText"/> の値へ戻した変更を通知しないので、今の文字が要るときはこのプロパティで読む。</summary>
    public string Text
    {
        get
        {
            ThrowIfDestroyed();
            return 本物.Text;
        }
    }

    /// <summary>今の選択範囲を返す。範囲を選んでいなければ、長さ 0 でカーソルの位置を表す。</summary>
    public TextSelection Selection
    {
        get
        {
            ThrowIfDestroyed();
            return new TextSelection(本物.SelectionStart, 本物.SelectionLength);
        }
    }

    /// <summary>カーソルの位置か選択範囲を設定する。この操作では変更を通知しない。文字の長さを超える分の扱いは WinUI に従う。負の値は例外になる。</summary>
    public TextBox SetSelection(TextSelection selection)
    {
        ThrowIfDestroyed();
        ArgumentOutOfRangeException.ThrowIfNegative(selection.Start);
        ArgumentOutOfRangeException.ThrowIfNegative(selection.Length);
        本物.Select(selection.Start, selection.Length);
        return this;
    }

    /// <summary>日本語入力などの変換の途中なら true。変換を確定する Enter や矢印を横取りしないために、キーを処理する前に読む。</summary>
    public bool IsComposing
    {
        get
        {
            ThrowIfDestroyed();
            return 変換中;
        }
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
