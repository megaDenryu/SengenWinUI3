namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>文字の入力欄。複数行かどうかは AcceptsReturn と TextWrapping で指定する。 見た目は Style で指定する。</summary>
    public static TextBox TextBox(string text, string placeholderText = "", bool acceptsReturn = false, TextWrapping textWrapping = TextWrapping.NoWrap, Style? style = null) =>
        StyleIfGiven(new TextBox(text, placeholderText, acceptsReturn, textWrapping), style);
}
