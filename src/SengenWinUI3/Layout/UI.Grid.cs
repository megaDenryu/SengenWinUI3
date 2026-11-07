namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>行と列を明示しない Grid を作る。子は既定の1区画に配置される。</summary>
    public static Grid Grid(Style? style = null) => Grid([], [], style);

    /// <summary>行の高さと列の幅を指定した Grid を作る。子は Children で入れる。</summary>
    public static Grid Grid(IEnumerable<GridLength> rowDefinitions, IEnumerable<GridLength> columnDefinitions, Style? style = null) =>
        StyleIfGiven(new Grid(rowDefinitions, columnDefinitions), style);
}
