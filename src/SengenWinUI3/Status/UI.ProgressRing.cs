namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>円で進みを表示する ProgressRing を作る。</summary>
    public static ProgressRing ProgressRing(bool isActive, ProgressState progress, Style? style = null) => StyleIfGiven(new ProgressRing(isActive, progress), style);
}
