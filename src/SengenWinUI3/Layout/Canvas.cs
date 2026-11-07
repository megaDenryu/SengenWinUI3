using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>子を指定した位置へ配置する部品。位置は子の CanvasLeft・CanvasTop で指定する。</summary>
public sealed class Canvas : Node, IHasChildren
{
    private static readonly StyleTarget<WinUI.Controls.Canvas> 当て方 =
        new(FrameworkElementStyle.Layout, PanelStyle.Background);

    private readonly WinUI.Controls.Canvas 本物 = new();

    internal Canvas() { }

    internal override WinUI.FrameworkElement Element => 本物;

    int IHasChildren.ChildCount => 本物.Children.Count;

    void IHasChildren.InsertChildElement(int index, Node child) => 本物.Children.Insert(index, child.Element);

    void IHasChildren.RemoveChildElement(Node child) => _ = 本物.Children.Remove(child.Element);

    void IHasChildren.ClearChildElements() => 本物.Children.Clear();

    internal override void ApplyStyle(Style style) => 当て方.Apply(style, 本物, this);
}
