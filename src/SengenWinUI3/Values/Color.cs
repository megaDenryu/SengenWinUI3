namespace SengenWinUI3;

/// <summary>不透明度と赤・緑・青の4つの成分(0〜255)で表す色。</summary>
public readonly record struct Color(byte A, byte R, byte G, byte B)
{
    internal Windows.UI.Color ToWinUI() => Windows.UI.Color.FromArgb(A, R, G, B);
}
