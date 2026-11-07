using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>塗り方。今は1色で塗る場合だけを持ち、テーマの色は使われた形から場合を足す。</summary>
public union Brush(Color)
{
    internal WinUI.Media.Brush ToWinUI() => this switch
    {
        Color 色 => new WinUI.Media.SolidColorBrush(色.ToWinUI()),
    };
}
