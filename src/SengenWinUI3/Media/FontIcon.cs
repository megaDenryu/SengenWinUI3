using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>フォントの字形を表示する部品。フォントと文字の見た目は Style で指定する。</summary>
public sealed class FontIcon : Node
{
    private readonly WinUI.Controls.FontIcon 本物 = new();
    private readonly ElementValue<WinUI.Controls.FontIcon, string> 字形;

    internal FontIcon(string glyph)
    {
        字形 = new(本物, static (本物, 値) => string.Equals(本物.Glyph, 値, StringComparison.Ordinal),
            static (本物, 値) => 本物.Glyph = 値);
        字形.Set(glyph);
    }

    internal override WinUI.FrameworkElement Element => 本物;

    /// <summary>表示する字形を替える。</summary>
    public FontIcon SetGlyph(string glyph)
    {
        ThrowIfDestroyed();
        字形.Set(glyph);
        return this;
    }

    internal override void ApplyStyle(Style style) => FontIconStyle.All.Apply(style, 本物, this);
}
