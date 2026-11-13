using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>TextBox の1つの状態(ふだん・重ねた・フォーカス・押せない)の背景・文字・薄い文字・枠線の塗り。null の項目は既定の色のままにする。</summary>
public readonly record struct TextBoxStateColors
{
    /// <summary>背景の塗り。</summary>
    public Brush? Background { get; init; }
    /// <summary>文字の塗り。</summary>
    public Brush? Foreground { get; init; }
    /// <summary>何も入力していないときに薄く出す文字(placeholderText)の塗り。</summary>
    public Brush? PlaceholderForeground { get; init; }
    /// <summary>枠線の塗り。</summary>
    public Brush? BorderBrush { get; init; }

    internal void WriteTo(WinUI.FrameworkElement 本物, string 状態の末尾)
    {
        LightweightResources.Write(本物, $"TextControlBackground{状態の末尾}", Background);
        LightweightResources.Write(本物, $"TextControlForeground{状態の末尾}", Foreground);
        LightweightResources.Write(本物, $"TextControlPlaceholderForeground{状態の末尾}", PlaceholderForeground);
        LightweightResources.Write(本物, $"TextControlBorderBrush{状態の末尾}", BorderBrush);
    }
}
