namespace SengenWinUI3;

/// <summary>線形のグラデーションの1つの区切り。始点を 0、終点を 1 としたときの位置 <paramref name="Offset"/> で、色が <paramref name="Color"/> になる。</summary>
public readonly record struct GradientStop(Color Color, Ratio Offset);
