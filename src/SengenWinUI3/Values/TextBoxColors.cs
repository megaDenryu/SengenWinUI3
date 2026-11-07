using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>TextBox の4つの状態ごとの色。<see cref="TextBox.SetColors"/> が使う。</summary>
public readonly record struct TextBoxColors
{
    /// <summary>ふだんの色。</summary>
    public TextBoxStateColors Normal { get; init; }
    /// <summary>ポインターを重ねたときの色。</summary>
    public TextBoxStateColors PointerOver { get; init; }
    /// <summary>フォーカスがあるときの色。</summary>
    public TextBoxStateColors Focused { get; init; }
    /// <summary>押せないときの色。</summary>
    public TextBoxStateColors Disabled { get; init; }

    internal void WriteTo(WinUI.FrameworkElement 本物)
    {
        Normal.WriteTo(本物, "");
        PointerOver.WriteTo(本物, "PointerOver");
        Focused.WriteTo(本物, "Focused");
        Disabled.WriteTo(本物, "Disabled");
    }
}
