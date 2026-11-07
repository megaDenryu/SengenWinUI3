namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>棒で進みを表示する ProgressBar を作る。</summary>
    public static ProgressBar ProgressBar(ProgressState progress, Style? style = null) => StyleIfGiven(new ProgressBar(progress), style);
}
