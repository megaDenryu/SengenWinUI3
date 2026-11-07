using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>子を1列に並べる部品。既定は WinUI 3 と同じく縦に並べ、向きと間隔は Style の Orientation と Spacing で決める。作る関数は <see cref="UI.StackPanel(Style?)"/>。</summary>
public sealed class StackPanel : Node, IHasChildren
{
    private static readonly StyleTarget<WinUI.Controls.StackPanel> 当て方 =
        new(FrameworkElementStyle.Layout, PanelStyle.Background, StackPanelStyle.Padding, StackPanelStyle.Border, StackPanelStyle.Arrangement);

    private readonly WinUI.Controls.StackPanel 本物 = new();

    internal StackPanel() { }

    internal override WinUI.FrameworkElement Element => 本物;

    int IHasChildren.ChildCount => 本物.Children.Count;

    void IHasChildren.InsertChildElement(int index, Node child) => 本物.Children.Insert(index, child.Element);

    void IHasChildren.ClearChildElements() => 本物.Children.Clear();

    internal override void ApplyStyle(Style style) => 当て方.Apply(style, 本物, this);

    void IHasChildren.RemoveChildElement(Node child) => _ = 本物.Children.Remove(child.Element);
}
