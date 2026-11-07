namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>収まらない分をスクロールで見せる <see cref="SengenWinUI3.ScrollViewer"/> を作る。中身は <c>.Content(…)</c> で置く。</summary>
    public static ScrollViewer ScrollViewer(Style? style = null) => StyleIfGiven(new ScrollViewer(), style);
}
