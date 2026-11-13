using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>子を行と列に配置する部品。子の位置は GridRow・GridColumn などのチェーンで設定する。</summary>
public sealed class Grid : Node, IHasChildren
{
    private readonly WinUI.Controls.Grid 本物 = new();

    internal Grid(IEnumerable<GridLength> rowDefinitions, IEnumerable<GridLength> columnDefinitions)
    {
        foreach (GridLength 高さ in rowDefinitions)
        {
            本物.RowDefinitions.Add(new WinUI.Controls.RowDefinition { Height = 高さ.ToWinUI() });
        }
        foreach (GridLength 幅 in columnDefinitions)
        {
            本物.ColumnDefinitions.Add(new WinUI.Controls.ColumnDefinition { Width = 幅.ToWinUI() });
        }
    }

    internal override WinUI.FrameworkElement Element => 本物;

    /// <summary>作るときに渡した行の高さを、同じ数の新しい高さに替える。数が違えば例外を投げる。</summary>
    public Grid SetRowDefinitions(GridLength[] rowDefinitions)
    {
        ThrowIfDestroyed();
        数が違えば例外を投げる(rowDefinitions.Length, 本物.RowDefinitions.Count, nameof(rowDefinitions));
        for (int 番号 = 0; 番号 < rowDefinitions.Length; 番号++)
        {
            本物.RowDefinitions[番号].Height = rowDefinitions[番号].ToWinUI();
        }
        return this;
    }

    /// <summary>作るときに渡した列の幅を、同じ数の新しい幅に替える。数が違えば例外を投げる。</summary>
    public Grid SetColumnDefinitions(GridLength[] columnDefinitions)
    {
        ThrowIfDestroyed();
        数が違えば例外を投げる(columnDefinitions.Length, 本物.ColumnDefinitions.Count, nameof(columnDefinitions));
        for (int 番号 = 0; 番号 < columnDefinitions.Length; 番号++)
        {
            本物.ColumnDefinitions[番号].Width = columnDefinitions[番号].ToWinUI();
        }
        return this;
    }

    int IHasChildren.ChildCount => 本物.Children.Count;

    void IHasChildren.InsertChildElement(int index, Node child) => 本物.Children.Insert(index, child.Element);

    void IHasChildren.RemoveChildElement(Node child) => _ = 本物.Children.Remove(child.Element);

    void IHasChildren.ClearChildElements() => 本物.Children.Clear();

    internal override void ApplyStyle(Style style) => GridStyle.All.Apply(style, 本物, this);

    private static void 数が違えば例外を投げる(int 渡した数, int 今の数, string 引数の名前)
    {
        if (渡した数 != 今の数)
        {
            throw new ArgumentException($"作るときの数({今の数})と違う数({渡した数})は渡せない", 引数の名前);
        }
    }
}
