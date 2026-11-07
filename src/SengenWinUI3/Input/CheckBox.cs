using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>2つの状態だけを持つチェック欄。</summary>
public sealed class CheckBox : Node, IControlNode
{
    private static readonly EventBinding<WinUI.Controls.CheckBox, WinUI.RoutedEventHandler, EventArgs> 変更の出来事 = new(
        static (本物, 呼ぶ) => (送信元, 出来事) => 呼ぶ(EventArgs.Empty),
        static (本物, ハンドラ) => { 本物.Checked += ハンドラ; 本物.Unchecked += ハンドラ; },
        static (本物, ハンドラ) => { 本物.Checked -= ハンドラ; 本物.Unchecked -= ハンドラ; });

    private readonly WinUI.Controls.CheckBox 本物 = new();
    private readonly InputValue<bool> 入っているか;

    internal CheckBox(bool isChecked, string content)
    {
        本物.Content = content;
        本物.IsThreeState = false;
        ElementValue<WinUI.Controls.CheckBox, bool> 設定先 = new(本物,
            static (本物, 値) => (本物.IsChecked == true).Equals(値),
            static (本物, 値) => 本物.IsChecked = 値);
        入っているか = new(isChecked, 設定先.Set);
        入っているか.Set(isChecked);
    }

    internal override WinUI.FrameworkElement Element => 本物;
    WinUI.Controls.Control IControlNode.Control => 本物;

    /// <summary>プログラムから値を設定する。この操作では変更を通知しない。</summary>
    public CheckBox SetIsChecked(bool isChecked)
    {
        ThrowIfDestroyed();
        入っているか.Set(isChecked);
        return this;
    }

    /// <summary>利用者の変更を通知する。最後の設定値へ戻した場合は通知しない。2回目の登録は例外になる。</summary>
    public CheckBox OnCheckedChanged(Action<bool> handler) =>
        this.WireEvent(nameof(OnCheckedChanged), 本物, 変更の出来事, () => 利用者の変更を知らせる(handler));

    internal override void ApplyStyle(Style style) => ControlStyle.All.Apply(style, 本物, this);

    private void 利用者の変更を知らせる(Action<bool> 呼ぶ)
    {
        bool 現在値 = 本物.IsChecked == true;
        if (入っているか.IsUserChange(現在値)) 呼ぶ(現在値);
    }
}
