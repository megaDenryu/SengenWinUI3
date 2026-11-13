namespace SengenWinUI3;

/// <summary>四隅で別々の半径を持つ角の丸み。左上から時計回りに、左上・右上・右下・左下の順に渡す。</summary>
public readonly record struct PerCornerRadius(Px TopLeft, Px TopRight, Px BottomRight, Px BottomLeft)
{
    internal Px ToUniformRadius() => TopLeft == TopRight && TopLeft == BottomRight && TopLeft == BottomLeft
        ? TopLeft
        : throw new InvalidOperationException("四隅の半径を1つにしかできない図形に、四隅で別々の角の丸みを当てた");
}
