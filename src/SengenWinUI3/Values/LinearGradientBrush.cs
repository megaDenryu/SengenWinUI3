using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>始点から終点へ向かう直線に沿って、区切りの色を混ぜて塗る塗り方。始点と終点は塗る図形の外接する長方形の中の位置で渡す。</summary>
public sealed record LinearGradientBrush(RelativePoint StartPoint, RelativePoint EndPoint, GradientStop[] GradientStops)
{
    internal WinUI.Media.LinearGradientBrush ToWinUI()
    {
        WinUI.Media.LinearGradientBrush 本物 = new() { StartPoint = StartPoint.ToWinUI(), EndPoint = EndPoint.ToWinUI() };
        foreach (GradientStop 区切り in GradientStops)
        {
            本物.GradientStops.Add(new WinUI.Media.GradientStop { Color = 区切り.Color.ToWinUI(), Offset = 区切り.Offset.Value });
        }
        return 本物;
    }
}
