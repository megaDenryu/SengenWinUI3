using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>パスワードの入力欄。</summary>
public sealed class PasswordBox : Node, IControlNode
{
    private static readonly EventBinding<WinUI.Controls.PasswordBox, WinUI.RoutedEventHandler, EventArgs> 変更の出来事 = new(
        static (本物, 呼ぶ) => (送信元, 出来事) => 呼ぶ(EventArgs.Empty),
        static (本物, ハンドラ) => 本物.PasswordChanged += ハンドラ,
        static (本物, ハンドラ) => 本物.PasswordChanged -= ハンドラ);

    private readonly WinUI.Controls.PasswordBox 本物 = new();
    private readonly InputValue<string> 文字;

    internal PasswordBox(string password, string placeholderText)
    {
        本物.PlaceholderText = placeholderText;
        ElementValue<WinUI.Controls.PasswordBox, string> 設定先 = new(本物,
            static (本物, 値) => (本物.Password).Equals(値),
            static (本物, 値) => 本物.Password = 値);
        文字 = new(password, 設定先.Set);
        文字.Set(password);
    }

    internal override WinUI.FrameworkElement Element => 本物;
    WinUI.Controls.Control IControlNode.Control => 本物;

    /// <summary>プログラムから値を設定する。この操作では変更を通知しない。</summary>
    public PasswordBox SetPassword(string password)
    {
        ThrowIfDestroyed();
        文字.Set(password);
        return this;
    }

    /// <summary>利用者の変更を通知する。最後の設定値へ戻した場合は通知しない。2回目の登録は例外になる。</summary>
    public PasswordBox OnPasswordChanged(Action<string> handler) =>
        this.WireEvent(nameof(OnPasswordChanged), 本物, 変更の出来事, () => 利用者の変更を知らせる(handler));

    internal override void ApplyStyle(Style style) => ControlStyle.All.Apply(style, 本物, this);

    private void 利用者の変更を知らせる(Action<string> 呼ぶ)
    {
        string 現在値 = 本物.Password;
        if (文字.IsUserChange(現在値)) 呼ぶ(現在値);
    }
}
