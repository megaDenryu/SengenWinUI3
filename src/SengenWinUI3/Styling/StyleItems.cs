namespace SengenWinUI3;

// Style の項目のうち null でないものの集まり。部品の種類ごとに持つ項目と比べ、部品が持たない項目を当てたかを調べる。
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
    Layout = Width | Height | MinWidth | MaxWidth | MinHeight | MaxHeight | Margin | HorizontalAlignment | VerticalAlignment | Opacity, // FrameworkElement が持つので、全部の葉が持つ
    ControlLook = Padding | Background | Foreground | BorderBrush | BorderThickness | CornerRadius | FontSize | FontWeight, // Control が持つ
}
