using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>Grid の行や列の長さ。自動・残りの長さの比率・Px のいずれかを渡す。</summary>
public union GridLength(GridAuto, GridStar, Px)
{
    internal WinUI.GridLength ToWinUI() => this switch
    {
        GridAuto => WinUI.GridLength.Auto,
        GridStar 比率 => new WinUI.GridLength(比率.Value, WinUI.GridUnitType.Star),
        Px 長さ => new WinUI.GridLength(長さ.Value, WinUI.GridUnitType.Pixel),
    };
}
