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
        記号.Set(ToWinUI(symbol));
    }

    internal override WinUI.FrameworkElement Element => 本物;

    /// <summary>表示する記号を替える。</summary>
    public SymbolIcon SetSymbol(Symbol symbol)
    {
        ThrowIfDestroyed();
        記号.Set(ToWinUI(symbol));
        return this;
    }

    internal override void ApplyStyle(Style style) => 当て方.Apply(style, 本物, this);

    private static WinUI.Controls.Symbol ToWinUI(Symbol 記号) => 記号 switch
    {
        Symbol.Add => WinUI.Controls.Symbol.Add,
        Symbol.Remove => WinUI.Controls.Symbol.Remove,
        Symbol.Delete => WinUI.Controls.Symbol.Delete,
        Symbol.Edit => WinUI.Controls.Symbol.Edit,
        Symbol.Save => WinUI.Controls.Symbol.Save,
        Symbol.Accept => WinUI.Controls.Symbol.Accept,
        Symbol.Cancel => WinUI.Controls.Symbol.Cancel,
        Symbol.Play => WinUI.Controls.Symbol.Play,
        Symbol.Pause => WinUI.Controls.Symbol.Pause,
        Symbol.Stop => WinUI.Controls.Symbol.Stop,
        Symbol.Back => WinUI.Controls.Symbol.Back,
        Symbol.Forward => WinUI.Controls.Symbol.Forward,
        Symbol.Home => WinUI.Controls.Symbol.Home,
        Symbol.Setting => WinUI.Controls.Symbol.Setting,
        Symbol.Refresh => WinUI.Controls.Symbol.Refresh,
        _ => throw new ArgumentOutOfRangeException(nameof(記号), 記号, null),
    };
}
