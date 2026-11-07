using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// 線の端と角の形を WinUI の値へ変える。
internal static class StrokeValueConversions
{
    internal static WinUI.Media.PenLineCap ToWinUI(this PenLineCap 端) => 端 switch
    {
        PenLineCap.Flat => WinUI.Media.PenLineCap.Flat,
        PenLineCap.Square => WinUI.Media.PenLineCap.Square,
        PenLineCap.Round => WinUI.Media.PenLineCap.Round,
        PenLineCap.Triangle => WinUI.Media.PenLineCap.Triangle,
        _ => throw new ArgumentOutOfRangeException(nameof(端), 端, null),
    };

    internal static WinUI.Media.PenLineJoin ToWinUI(this PenLineJoin 角) => 角 switch
    {
        PenLineJoin.Miter => WinUI.Media.PenLineJoin.Miter,
        PenLineJoin.Bevel => WinUI.Media.PenLineJoin.Bevel,
        PenLineJoin.Round => WinUI.Media.PenLineJoin.Round,
        _ => throw new ArgumentOutOfRangeException(nameof(角), 角, null),
    };
}
