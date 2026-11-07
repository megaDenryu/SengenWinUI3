namespace SengenWinUI3;

/// <summary>Grid の残りの長さを分ける重み。Value は長さ同士の比なので、単位を持たない数で受ける。</summary>
public readonly record struct GridStar(double Value);
