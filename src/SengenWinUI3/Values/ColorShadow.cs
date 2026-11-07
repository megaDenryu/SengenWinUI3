namespace SengenWinUI3;

/// <summary>形から出す色つきの光(影)。色の不透明度は光の濃さになる。広がり(CSS の spread)は持たない。</summary>
/// <param name="Color">光の色。</param>
/// <param name="BlurRadius">ぼかしの半径。</param>
/// <param name="Offset">形からの横縦のずれ。</param>
public readonly record struct ColorShadow(Color Color, Px BlurRadius, Point Offset);
