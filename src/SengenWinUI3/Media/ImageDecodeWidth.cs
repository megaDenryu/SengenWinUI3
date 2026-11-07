namespace SengenWinUI3;

/// <summary>画像を幅 <paramref name="Width"/> に縮めて読み込む。小さく見せる大きな画像のメモリを減らすために使う。</summary>
public readonly record struct ImageDecodeWidth(Px Width);
