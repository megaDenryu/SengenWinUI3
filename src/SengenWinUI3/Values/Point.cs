namespace SengenWinUI3;

/// <summary>横と縦の2つの長さで表す点。横は右、縦は下が正である。Popup の基準からのずれ・Line の端・Translation のずれに使う。</summary>
public readonly record struct Point(Px X, Px Y);
