namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>子の測り方と置き方を <paramref name="layout"/> に任せる LayoutPanel を作る。子は Children で入れる。</summary>
    public static LayoutPanel LayoutPanel(NonVirtualizingLayout layout, Style? style = null) => StyleIfGiven(new LayoutPanel(layout), style);
}
