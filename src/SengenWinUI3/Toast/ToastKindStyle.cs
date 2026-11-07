namespace SengenWinUI3;

/// <summary>
/// 1つの種類のトーストの見た目。<paramref name="Mark"/> は先頭に出す印の文字。
/// <paramref name="Style"/> のうち、Background・BorderBrush・Foreground・BorderThickness・CornerRadius・Padding・FontSize を使い、null の項目は既定の値にする。
/// </summary>
public sealed record ToastKindStyle(string Mark, Style Style);
