using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>ToggleButton の、切れているときと入っているときの状態ごとの色。<see cref="ToggleButton.SetColors"/> が使う。</summary>
public readonly record struct ToggleButtonColors
{
    /// <summary>切れているときの色。</summary>
    public ButtonColors Unchecked { get; init; }
    /// <summary>入っているときの色。</summary>
    public ButtonColors Checked { get; init; }

    internal void WriteTo(WinUI.FrameworkElement 本物)
    {
        Unchecked.WriteTo(本物, "ToggleButton", "");
        Checked.WriteTo(本物, "ToggleButton", "Checked");
    }
}
