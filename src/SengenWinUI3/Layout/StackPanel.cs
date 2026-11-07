using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>子を1列に並べる部品。既定は WinUI 3 と同じく縦に並べ、向きと間隔は Style の Orientation と Spacing で決める。作る関数は <see cref="UI.StackPanel(Style?)"/>。</summary>
public sealed class StackPanel : Node, IHasChildren
{
    private const StyleItems 持つ項目 = StyleItems.Layout | StyleItems.Padding | StyleItems.Background | StyleItems.BorderBrush
        | StyleItems.BorderThickness | StyleItems.CornerRadius | StyleItems.Orientation | StyleItems.Spacing;

    private readonly WinUI.Controls.StackPanel 本物 = new();

    internal StackPanel() { }

    internal override WinUI.FrameworkElement Element => 本物;

    WinUI.Controls.Panel IHasChildren.ChildPanel => 本物;

    internal override void ApplyStyle(Style style)
    {
        style.ThrowIfUnsupported(持つ項目, this);
        style.ApplyLayout(本物);
        if (style.Padding is { } 内側の余白) 本物.Padding = 内側の余白.ToWinUI();
        if (style.BackgroundBrush() is { } 背景) 本物.Background = 背景;
        if (style.BorderBrushBrush() is { } 枠線の色) 本物.BorderBrush = 枠線の色;
        if (style.BorderThickness is { } 枠線の太さ) 本物.BorderThickness = 枠線の太さ.ToWinUI();
        if (style.CornerRadius is { } 丸み) 本物.CornerRadius = new WinUI.CornerRadius(丸み.Value);
        if (style.Orientation is { } 向き) 本物.Orientation = 向き.ToWinUI();
        if (style.Spacing is { } 間隔) 本物.Spacing = 間隔.Value;
    }

    internal override void RemoveChildElement(Node child) => _ = 本物.Children.Remove(child.Element);
}
