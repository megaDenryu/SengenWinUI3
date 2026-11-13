namespace SengenWinUI3;

/// <summary>
/// 画素から作る画像の元。1画素を青・緑・赤・不透明度の順に1バイトずつ持ち、左上から行ごとに続ける。色には不透明度を掛けてある。
/// バイト列の長さは、幅 × 高さ × 4 でなければならない。
/// </summary>
public readonly record struct BgraPixels(int Width, int Height, ReadOnlyMemory<byte> PremultipliedBytes);
