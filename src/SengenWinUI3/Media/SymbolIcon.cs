using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>標準の記号を表示する部品。色と配置は Style で指定する。</summary>
public sealed class SymbolIcon : Node
{
    private static readonly StyleTarget<WinUI.Controls.SymbolIcon> 当て方 =
        new(FrameworkElementStyle.Layout, IconElementStyle.Foreground);
    private readonly WinUI.Controls.SymbolIcon 本物 = new();
    private readonly ElementValue<WinUI.Controls.SymbolIcon, WinUI.Controls.Symbol> 記号;

    internal SymbolIcon(Symbol symbol)
    {
        記号 = new(本物, static (本物, 値) => 本物.Symbol == 値, static (本物, 値) => 本物.Symbol = 値);
        記号.Set(symbol.ToWinUI());
    }

    internal override WinUI.FrameworkElement Element => 本物;

    /// <summary>表示する記号を替える。</summary>
    public SymbolIcon SetSymbol(Symbol symbol)
    {
        ThrowIfDestroyed();
        記号.Set(symbol.ToWinUI());
        return this;
    }

    internal override void ApplyStyle(Style style) => 当て方.Apply(style, 本物, this);
}
