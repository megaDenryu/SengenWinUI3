namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>指定した字形の FontIcon を作る。</summary>
    public static FontIcon FontIcon(string glyph, Style? style = null) => StyleIfGiven(new FontIcon(glyph), style);
}
