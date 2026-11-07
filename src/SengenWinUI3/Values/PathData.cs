using Microsoft.UI.Xaml.Markup;
using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>SVG の道の文字列(<c>M12 5v14M5 12h14</c> のような形)で書いた図形の輪郭。<see cref="Path"/> が形にする。</summary>
public readonly record struct PathData(string Value)
{
    // 書き方が正しくない文字列は、WinUI が例外を投げる。書いた側の誤りなので、そのまま上へ出す。
    internal WinUI.Media.Geometry ToWinUI() =>
        XamlBindingHelper.ConvertValue(typeof(WinUI.Media.Geometry), Value) is WinUI.Media.Geometry 形
            ? 形
            : throw new InvalidOperationException($"道の文字列を図形に変えられなかった: {Value}");
}
