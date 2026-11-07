using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>Grid の子の行・列・またぐ区画の数を指定するチェーン。元の具象型を保つ。</summary>
public static class GridPlacementExtensions
{
    /// <summary>子の行を0から数えた番号で指定する。</summary>
    public static T GridRow<T>(this T node, int row) where T : Node
    {
        node.ThrowIfDestroyed();
        WinUI.Controls.Grid.SetRow(node.Element, row);
        return node;
    }

    /// <summary>子の列を0から数えた番号で指定する。</summary>
    public static T GridColumn<T>(this T node, int column) where T : Node
    {
        node.ThrowIfDestroyed();
        WinUI.Controls.Grid.SetColumn(node.Element, column);
        return node;
    }

    /// <summary>子がまたぐ行の数を指定する。</summary>
    public static T GridRowSpan<T>(this T node, int rowSpan) where T : Node
    {
        node.ThrowIfDestroyed();
        WinUI.Controls.Grid.SetRowSpan(node.Element, rowSpan);
        return node;
    }

    /// <summary>子がまたぐ列の数を指定する。</summary>
    public static T GridColumnSpan<T>(this T node, int columnSpan) where T : Node
    {
        node.ThrowIfDestroyed();
        WinUI.Controls.Grid.SetColumnSpan(node.Element, columnSpan);
        return node;
    }
}
