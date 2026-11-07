using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>数を選ぶスライダー。Value・Minimum・Maximum・StepFrequency は長さや割合に限定しない単位の無い数である。</summary>
public sealed class Slider : Node, IControlNode
{
    private static readonly EventBinding<WinUI.Controls.Slider, WinUI.Controls.Primitives.RangeBaseValueChangedEventHandler, EventArgs> 変更の出来事 = new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(EventArgs.Empty),
        static (本物, ハンドラ) => 本物.ValueChanged += ハンドラ,
        static (本物, ハンドラ) => 本物.ValueChanged -= ハンドラ);

    private readonly WinUI.Controls.Slider 本物 = new();
    private readonly InputValue<double> 数値;

    internal Slider(double value, double minimum, double maximum, double stepFrequency)
    {
        本物.Minimum = minimum;
        本物.Maximum = maximum;
        本物.StepFrequency = stepFrequency;
        数値 = InputValue.Create(本物, Math.Clamp(value, 本物.Minimum, 本物.Maximum),
            static (本物, 値) => (本物.Value).Equals(値),
            static (本物, 値) => 本物.Value = 値);
    }

    internal override WinUI.FrameworkElement Element => 本物;
    WinUI.Controls.Control IControlNode.Control => 本物;

    /// <summary>プログラムから値を設定する。この操作では変更を通知しない。</summary>
    public Slider SetValue(double value)
    {
        ThrowIfDestroyed();
        // WinUI が範囲内へ収めた値も Set の通知として除くため、比較する値を先にそろえる。
        数値.Set(Math.Clamp(value, 本物.Minimum, 本物.Maximum));
        return this;
    }

    /// <summary>利用者の変更を通知する。最後の設定値へ戻した場合は通知しない。2回目の登録は例外になる。</summary>
    public Slider OnValueChanged(Action<double> handler) =>
        this.WireEvent(nameof(OnValueChanged), 本物, 変更の出来事, () => 利用者の変更を知らせる(handler));

    internal override void ApplyStyle(Style style) => ControlStyle.All.Apply(style, 本物, this);

    private void 利用者の変更を知らせる(Action<double> 呼ぶ)
    {
        double 今の数値 = 本物.Value;
        if (数値.IsUserChange(今の数値)) 呼ぶ(今の数値);
    }
}
