namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>長方形を表示する Rectangle を作る。</summary>
    public static Rectangle Rectangle(Style? style = null) => StyleIfGiven(new Rectangle(), style);
}
