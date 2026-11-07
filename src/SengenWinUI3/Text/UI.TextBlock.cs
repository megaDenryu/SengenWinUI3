using SengenWinUI3.State;

namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>固定の文字を見せる <see cref="SengenWinUI3.TextBlock"/> を作る。</summary>
    public static TextBlock TextBlock(string text, Style? style = null) => StyleIfGiven(new TextBlock(text), style);

    /// <summary>セルの文字を見せ、セルが変わるたびに見せ直す <see cref="SengenWinUI3.TextBlock"/> を作る。</summary>
    public static TextBlock TextBlock(IReadOnlyCell<string> text, Style? style = null) => StyleIfGiven(new TextBlock(text), style);
}
