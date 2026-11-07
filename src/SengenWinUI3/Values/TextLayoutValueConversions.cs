using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// 文字の寄せ方と切り詰め方を WinUI の値へ変える。
internal static class TextLayoutValueConversions
{
    internal static WinUI.TextAlignment ToWinUI(this TextAlignment 寄せ方) => 寄せ方 switch
    {
        TextAlignment.Left => WinUI.TextAlignment.Left,
        TextAlignment.Center => WinUI.TextAlignment.Center,
        TextAlignment.Right => WinUI.TextAlignment.Right,
        TextAlignment.Justify => WinUI.TextAlignment.Justify,
        _ => throw new ArgumentOutOfRangeException(nameof(寄せ方), 寄せ方, null),
    };

    internal static WinUI.TextTrimming ToWinUI(this TextTrimming 切り詰め方) => 切り詰め方 switch
    {
        TextTrimming.None => WinUI.TextTrimming.None,
        TextTrimming.Clip => WinUI.TextTrimming.Clip,
        TextTrimming.CharacterEllipsis => WinUI.TextTrimming.CharacterEllipsis,
        TextTrimming.WordEllipsis => WinUI.TextTrimming.WordEllipsis,
        _ => throw new ArgumentOutOfRangeException(nameof(切り詰め方), 切り詰め方, null),
    };
}
