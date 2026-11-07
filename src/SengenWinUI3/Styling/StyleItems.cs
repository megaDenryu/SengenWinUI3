namespace SengenWinUI3;

// Style の項目の印。部品が持つ項目の印は、部品が使う当て方(IStyleGroup)の印を合わせて作る。
[Flags]
internal enum StyleItems : long
{
    None = 0,
    Width = 1L << 0,
    Height = 1L << 1,
    MinWidth = 1L << 2,
    MaxWidth = 1L << 3,
    MinHeight = 1L << 4,
    MaxHeight = 1L << 5,
    Margin = 1L << 6,
    HorizontalAlignment = 1L << 7,
    VerticalAlignment = 1L << 8,
    Opacity = 1L << 9,
    Padding = 1L << 10,
    Orientation = 1L << 11,
    Spacing = 1L << 12,
    Background = 1L << 13,
    Foreground = 1L << 14,
    BorderBrush = 1L << 15,
    BorderThickness = 1L << 16,
    CornerRadius = 1L << 17,
    FontSize = 1L << 18,
    FontWeight = 1L << 19,
    TextWrapping = 1L << 20,
    IsTextSelectionEnabled = 1L << 21,
    RowSpacing = 1L << 22,
    ColumnSpacing = 1L << 23,
    HorizontalContentAlignment = 1L << 24,
    VerticalContentAlignment = 1L << 25,
    FontFamily = 1L << 26,
    Fill = 1L << 27,
    Stroke = 1L << 28,
    StrokeThickness = 1L << 29,
}
