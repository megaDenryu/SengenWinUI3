namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>行と列を明示しない Grid を作る。子は既定の1区画に配置される。</summary>
    public static Grid Grid(Style? style = null) => Grid([], [], style);

    /// <summary>行の高さと列の幅を指定した Grid を作る。子は Children で入れる。</summary>
    // 注意: 引数を配列にするのは、利用する側が [..] の式で書いたときに、CsWinRT の解析(CsWinRT1032)が読み取り専用のインターフェースへの式を誤りにするためである。
    public static Grid Grid(GridLength[] rowDefinitions, GridLength[] columnDefinitions, Style? style = null) =>
        StyleIfGiven(new Grid(rowDefinitions, columnDefinitions), style);
}
