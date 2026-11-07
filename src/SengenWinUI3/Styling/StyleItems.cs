namespace SengenWinUI3;

// Style の項目の印。部品が持つ項目の印は、部品が使う当て方(IStyleGroup)の印を合わせて作る。
[Flags]
internal enum StyleItems
{
    None = 0,
    Width = 1 << 0,
    Height = 1 << 1,
    MinWidth = 1 << 2,
    MaxWidth = 1 << 3,
    MinHeight = 1 << 4,
    MaxHeight = 1 << 5,
    Margin = 1 << 6,
    HorizontalAlignment = 1 << 7,
    VerticalAlignment = 1 << 8,
    Opacity = 1 << 9,
    Padding = 1 << 10,
    Orientation = 1 << 11,
    Spacing = 1 << 12,
    Background = 1 << 13,
    Foreground = 1 << 14,
    BorderBrush = 1 << 15,
    BorderThickness = 1 << 16,
    CornerRadius = 1 << 17,
    FontSize = 1 << 18,
    FontWeight = 1 << 19,
    TextWrapping = 1 << 20,
    IsTextSelectionEnabled = 1 << 21,
    RowSpacing = 1 << 22,
    ColumnSpacing = 1 << 23,
    HorizontalContentAlignment = 1 << 24,
    VerticalContentAlignment = 1 << 25,
    FontFamily = 1 << 26,
    Fill = 1 << 27,
    Stroke = 1 << 28,
    StrokeThickness = 1 << 29,
}
