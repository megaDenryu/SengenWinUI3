using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>ボタンの4つの状態ごとの色。<see cref="Button.SetColors"/> と <see cref="ToggleButtonColors"/> が使う。</summary>
public readonly record struct ButtonColors
{
    /// <summary>ふだんの色。</summary>
    public ButtonStateColors Normal { get; init; }
    /// <summary>ポインターを重ねたときの色。</summary>
    public ButtonStateColors PointerOver { get; init; }
    /// <summary>押している間の色。</summary>
    public ButtonStateColors Pressed { get; init; }
    /// <summary>押せないときの色。</summary>
    public ButtonStateColors Disabled { get; init; }

    // 選ばれた状態の中辞は、ToggleButton が入ったときの色に「Checked」を渡すためにある。ふつうのボタンは空にする。
    internal void WriteTo(WinUI.FrameworkElement 本物, string 資源名の頭, string 選ばれた状態の中辞)
    {
        Normal.WriteTo(本物, 資源名の頭, 選ばれた状態の中辞);
        PointerOver.WriteTo(本物, 資源名の頭, $"{選ばれた状態の中辞}PointerOver");
        Pressed.WriteTo(本物, 資源名の頭, $"{選ばれた状態の中辞}Pressed");
        Disabled.WriteTo(本物, 資源名の頭, $"{選ばれた状態の中辞}Disabled");
    }
}
