namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>固定の文字を表示する <see cref="SengenWinUI3.TextBlock"/> を作る。</summary>
    public static TextBlock TextBlock(string text, Style? style = null) => StyleIfGiven(new TextBlock(text), style);
}
