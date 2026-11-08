using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>基準の左上から offset だけずれた位置。開くときに基準の位置を求め、HorizontalOffset と VerticalOffset に書く。</summary>
internal sealed record OffsetFromAnchor(Point Offset);

/// <summary>基準の上へ、左端をそろえて出す位置。WinUI の PlacementTarget と DesiredPlacement に任せ、HorizontalOffset と VerticalOffset は書かない。</summary>
internal sealed record AboveAnchor;

/// <summary>Popup を基準のどこへ出すか。</summary>
internal union PopupPlacement(OffsetFromAnchor, AboveAnchor)
{
    // 位置に要る設定を本物へ書く。XamlRoot と IsOpen は呼ぶ側が書く。
    internal void ApplyTo(WinUI.Controls.Primitives.Popup 本物, WinUI.FrameworkElement 基準, WinUI.XamlRoot 根)
    {
        Action 書く = this switch
        {
            OffsetFromAnchor 指定 => () => 基準からのずれで置く(本物, 基準, 根, 指定.Offset),
            AboveAnchor _ => () => 基準の上に置く(本物, 基準),
        };
        書く();
    }

    private static void 基準からのずれで置く(WinUI.Controls.Primitives.Popup 本物, WinUI.FrameworkElement 基準, WinUI.XamlRoot 根, Point ずれ)
    {
        Windows.Foundation.Point 位置 = 基準.TransformToVisual(根.Content).TransformPoint(new Windows.Foundation.Point(0, 0));
        本物.HorizontalOffset = 位置.X + ずれ.X.Value;
        本物.VerticalOffset = 位置.Y + ずれ.Y.Value;
    }

    private static void 基準の上に置く(WinUI.Controls.Primitives.Popup 本物, WinUI.FrameworkElement 基準)
    {
        本物.PlacementTarget = 基準;
        本物.DesiredPlacement = WinUI.Controls.Primitives.PopupPlacementMode.TopEdgeAlignedLeft;
    }
}
