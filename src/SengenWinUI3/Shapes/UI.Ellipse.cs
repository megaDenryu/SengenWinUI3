namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>楕円を表示する Ellipse を作る。</summary>
    public static Ellipse Ellipse(Style? style = null) => StyleIfGiven(new Ellipse(), style);
}
