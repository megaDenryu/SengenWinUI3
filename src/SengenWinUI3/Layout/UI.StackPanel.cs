namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>子を1列に並べる <see cref="SengenWinUI3.StackPanel"/> を作る。子は <c>.Children(…)</c> で入れる。</summary>
    public static StackPanel StackPanel(Style? style = null) => StyleIfGiven(new StackPanel(), style);
}
