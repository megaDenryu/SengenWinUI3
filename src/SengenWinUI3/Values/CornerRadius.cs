using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>角の丸み。四隅を同じ半径にする <see cref="Px"/> か、四隅で別々の半径を持つ <see cref="PerCornerRadius"/> のどちらかを渡す。</summary>
public union CornerRadius(Px, PerCornerRadius)
{
    internal WinUI.CornerRadius ToWinUI() => this switch
    {
        Px 半径 => new WinUI.CornerRadius(半径.Value),
        PerCornerRadius 四隅 => new WinUI.CornerRadius(四隅.TopLeft.Value, 四隅.TopRight.Value, 四隅.BottomRight.Value, 四隅.BottomLeft.Value),
    };

    // 四隅の半径を1つにしかできない図形(Rectangle の RadiusX と RadiusY)が使う。四隅が別々の半径なら例外を投げる。
    internal Px ToUniformRadius() => this switch
    {
        Px 半径 => 半径,
        PerCornerRadius 四隅 => 四隅.ToUniformRadius(),
    };
}
