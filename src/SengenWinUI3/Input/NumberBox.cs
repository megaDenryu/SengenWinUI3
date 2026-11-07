using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>単位を持たない数の入力欄であるため、Value・Minimum・Maximum は double で受ける。</summary>
public sealed class NumberBox : Node, IControlNode
{
    private static readonly EventBinding<WinUI.Controls.NumberBox, Windows.Foundation.TypedEventHandler<WinUI.Controls.NumberBox, WinUI.Controls.NumberBoxValueChangedEventArgs>, EventArgs> 変更の出来事 = new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(EventArgs.Empty),
        static (本物, ハンドラ) => 本物.ValueChanged += ハンドラ,
        static (本物, ハンドラ) => 本物.ValueChanged -= ハンドラ);

    private readonly WinUI.Controls.NumberBox 本物 = new();
    private readonly InputValue<double> 数値;

    internal NumberBox(double value, double minimum, double maximum)
    {
        本物.Minimum = minimum;
        本物.Maximum = maximum;
        数値 = InputValue.Create(本物, Math.Clamp(value, 本物.Minimum, 本物.Maximum),
            static (本物, 値) => (本物.Value).Equals(値),
            static (本物, 値) => 本物.Value = 値);
    }

    internal override WinUI.FrameworkElement Element => 本物;
    WinUI.Controls.Control IControlNode.Control => 本物;

    /// <summary>プログラムから値を設定する。この操作では変更を通知しない。</summary>
    public NumberBox SetValue(double value)
    {
        ThrowIfDestroyed();
        // WinUI が範囲内へ収めた値も Set の通知として除くため、比較する値を先にそろえる。
        数値.Set(Math.Clamp(value, 本物.Minimum, 本物.Maximum));
        return this;
    }

    /// <summary>利用者の変更を通知する。最後の設定値へ戻した場合は通知しない。2回目の登録は例外になる。</summary>
    public NumberBox OnValueChanged(Action<double> handler) =>
        this.WireEvent(nameof(OnValueChanged), 本物, 変更の出来事, () => 利用者の変更を知らせる(handler));

    internal override void ApplyStyle(Style style) => ControlStyle.All.Apply(style, 本物, this);

    private void 利用者の変更を知らせる(Action<double> 呼ぶ)
    {
        double 今の数値 = 本物.Value;
        if (数値.IsUserChange(今の数値)) 呼ぶ(今の数値);
    }
}
