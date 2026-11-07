namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>文字を見せる <see cref="SengenWinUI3.Button"/> を作る。</summary>
    public static Button Button(string text, Style? style = null) => StyleIfGiven(new Button(text), style);

    /// <summary>中身の部品を見せる <see cref="SengenWinUI3.Button"/> を作る。親を持つ部品を渡すと例外を投げる。</summary>
    public static Button Button(Node content, Style? style = null) => StyleIfGiven(new Button(content), style);
}
