using Windows.Foundation;
using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>色を選ぶ部品。色は自前の Color で受け渡す。</summary>
public sealed class ColorPicker : Node, IControlNode
{
    private static readonly EventBinding<WinUI.Controls.ColorPicker, TypedEventHandler<WinUI.Controls.ColorPicker, WinUI.Controls.ColorChangedEventArgs>, EventArgs> 色の出来事 = new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(EventArgs.Empty),
        static (本物, ハンドラ) => 本物.ColorChanged += ハンドラ,
        static (本物, ハンドラ) => 本物.ColorChanged -= ハンドラ);

    private readonly WinUI.Controls.ColorPicker 本物 = new();
    private readonly InputValue<Color> 色;

    internal ColorPicker(Color color)
    {
        色 = InputValue.Create(本物, color,
            static (本物, 値) => Color.FromWinUI(本物.Color) == 値,
            static (本物, 値) => 本物.Color = 値.ToWinUI());
    }

    internal override WinUI.FrameworkElement Element => 本物;

    WinUI.Controls.Control IControlNode.Control => 本物;

    /// <summary>色を替える。この操作では OnColorChanged を呼ばない。</summary>
    public ColorPicker SetColor(Color color)
    {
        ThrowIfDestroyed();
        色.Set(color);
        return this;
    }

    /// <summary>利用者が選んだ色を知らせる。最後の設定値と等しい現在値は知らせない。2回目の登録は例外を投げる。</summary>
    public ColorPicker OnColorChanged(Action<Color> handler) =>
        this.WireEvent(nameof(OnColorChanged), 本物, 色の出来事, () => 色を知らせる(handler));

    internal override void ApplyStyle(Style style) => ControlStyle.All.Apply(style, 本物, this);

    private void 色を知らせる(Action<Color> 選んだ色を受ける)
    {
        Color 現在値 = Color.FromWinUI(本物.Color);
        if (色.IsUserChange(現在値)) 選んだ色を受ける(現在値);
    }
}
