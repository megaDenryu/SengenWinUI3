namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>中身の部品の収まらない分をスクロールで見せる <see cref="SengenWinUI3.ScrollViewer"/> を作る。親を持つ部品を渡すと例外を投げる。</summary>
    public static ScrollViewer ScrollViewer(Node content, Style? style = null) => StyleIfGiven(new ScrollViewer(content), style);
}
