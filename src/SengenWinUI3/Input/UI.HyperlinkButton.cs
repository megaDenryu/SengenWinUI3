namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>押したときの処理を OnClick で受けるリンクを作る。</summary>
    public static HyperlinkButton HyperlinkButton(string content, Style? style = null) =>
        StyleIfGiven(new HyperlinkButton(content), style);

    /// <summary>指定した URI を開くリンクを作る。</summary>
    public static HyperlinkButton HyperlinkButton(string content, Uri navigateUri, Style? style = null) =>
        StyleIfGiven(new HyperlinkButton(content, navigateUri), style);
}
