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

    int IHasChildren.ChildCount => 本物.Children.Count;

    void IHasChildren.InsertChildElement(int index, Node child) => 本物.Children.Insert(index, child.Element);

    void IHasChildren.RemoveChildElement(Node child) => _ = 本物.Children.Remove(child.Element);

    void IHasChildren.ClearChildElements() => 本物.Children.Clear();

    internal override void ApplyStyle(Style style) => GridStyle.All.Apply(style, 本物, this);
}
