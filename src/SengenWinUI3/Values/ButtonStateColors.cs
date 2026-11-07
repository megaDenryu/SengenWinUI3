using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>ボタンの1つの状態(ふだん・重ねた・押した・押せない)の背景・文字・枠線の塗り。null の項目は既定の色のままにする。</summary>
public readonly record struct ButtonStateColors
{
    /// <summary>背景の塗り。</summary>
    public Brush? Background { get; init; }
    /// <summary>文字の塗り。</summary>
    public Brush? Foreground { get; init; }
    /// <summary>枠線の塗り。</summary>
    public Brush? BorderBrush { get; init; }

    // 資源名は「頭 + 項目 + 状態の末尾」(ButtonBackgroundPointerOver など)。
    internal void WriteTo(WinUI.FrameworkElement 本物, string 資源名の頭, string 状態の末尾)
    {
        LightweightResources.Write(本物, $"{資源名の頭}Background{状態の末尾}", Background);
        LightweightResources.Write(本物, $"{資源名の頭}Foreground{状態の末尾}", Foreground);
        LightweightResources.Write(本物, $"{資源名の頭}BorderBrush{状態の末尾}", BorderBrush);
    }
}
