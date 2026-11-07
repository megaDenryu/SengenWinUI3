namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>指定した標準記号の SymbolIcon を作る。</summary>
    public static SymbolIcon SymbolIcon(Symbol symbol, Style? style = null) => StyleIfGiven(new SymbolIcon(symbol), style);
}
