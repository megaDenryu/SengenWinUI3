using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>塗り方。1色で塗る場合と、線形のグラデーションで塗る場合を持つ。テーマの色は使われた形から場合を足す。</summary>
public union Brush(Color, LinearGradientBrush)
{
    internal WinUI.Media.Brush ToWinUI() => this switch
    {
        Color 色 => new WinUI.Media.SolidColorBrush(色.ToWinUI()),
        LinearGradientBrush 勾配 => 勾配.ToWinUI(),
    };
}
