using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>
/// 部品の見た目。全部の項目が null を許し、null の項目は当てない。<c>static readonly Style</c> の定数に名前を付けて共有し、<see cref="With"/> で派生を作る。
/// 部品が持たない項目を null でなくした Style をその部品へ当てると、例外を投げる。
/// </summary>
public sealed class Style
{
    // 注意: ブラシは初めて当てるときに作り、同じ Style を当てた部品で共有する。WinUI のブラシは作ったスレッドに結び付くので、1つの Style は1つの UI スレッドの部品にだけ当てる。
    private WinUI.Media.Brush? 背景のブラシ;
    private WinUI.Media.Brush? 文字のブラシ;
    private WinUI.Media.Brush? 枠線のブラシ;
    private WinUI.Media.Brush? 図形のブラシ;
    private WinUI.Media.Brush? 輪郭のブラシ;
    private WinUI.Media.FontFamily? フォント;

    /// <summary>幅。</summary>
    public Px? Width { get; init; }
    /// <summary>高さ。</summary>
    public Px? Height { get; init; }
    /// <summary>最小の幅。</summary>
    public Px? MinWidth { get; init; }
    /// <summary>最大の幅。</summary>
    public Px? MaxWidth { get; init; }
    /// <summary>最小の高さ。</summary>
    public Px? MinHeight { get; init; }
    /// <summary>最大の高さ。</summary>
    public Px? MaxHeight { get; init; }
    /// <summary>外側の余白。</summary>
    public Thickness? Margin { get; init; }
    /// <summary>内側の余白。</summary>
    public Thickness? Padding { get; init; }
    /// <summary>親から与えられた幅の中での横の寄せ方。</summary>
    public HorizontalAlignment? HorizontalAlignment { get; init; }
    /// <summary>親から与えられた高さの中での縦の寄せ方。</summary>
    public VerticalAlignment? VerticalAlignment { get; init; }
    /// <summary>子を並べる向き(StackPanel)。</summary>
    public Orientation? Orientation { get; init; }
    /// <summary>子と子の間隔(StackPanel)。</summary>
    public Px? Spacing { get; init; }
    /// <summary>背景の塗り。</summary>
    public Brush? Background { get; init; }
    /// <summary>文字の塗り。</summary>
    public Brush? Foreground { get; init; }
    /// <summary>枠線の塗り。</summary>
    public Brush? BorderBrush { get; init; }
    /// <summary>枠線の太さ。</summary>
    public Thickness? BorderThickness { get; init; }
    /// <summary>角の丸み。<c>8.Px</c> と書くと四隅が同じ半径になり、<see cref="PerCornerRadius"/> を渡すと四隅で別々の半径になる。</summary>
    public CornerRadius? CornerRadius { get; init; }
    /// <summary>文字の大きさ。</summary>
    public Px? FontSize { get; init; }
    /// <summary>文字の太さ。</summary>
    public FontWeight? FontWeight { get; init; }
    /// <summary>幅に収まらない文字の折り返し方(TextBlock)。</summary>
    public TextWrapping? TextWrapping { get; init; }
    /// <summary>利用者が文字を選んで写せるか(TextBlock)。</summary>
    public bool? IsTextSelectionEnabled { get; init; }
    /// <summary>文字の行を幅の中で寄せる位置(TextBlock)。</summary>
    public TextAlignment? TextAlignment { get; init; }
    /// <summary>幅に収まらない文字のはみ出しの切り詰め方(TextBlock)。</summary>
    public TextTrimming? TextTrimming { get; init; }
    /// <summary>文字の1行の高さ(TextBlock)。</summary>
    public Px? LineHeight { get; init; }
    /// <summary>不透明度。</summary>
    public Ratio? Opacity { get; init; }
    /// <summary>Grid の行の間隔。</summary>
    public Px? RowSpacing { get; init; }
    /// <summary>Grid の列の間隔。</summary>
    public Px? ColumnSpacing { get; init; }
    /// <summary>Control の中身の横の寄せ方。</summary>
    public HorizontalAlignment? HorizontalContentAlignment { get; init; }
    /// <summary>Control の中身の縦の寄せ方。</summary>
    public VerticalAlignment? VerticalContentAlignment { get; init; }
    /// <summary>フォント名か、フォントのリソースを指定する文字列。</summary>
    public string? FontFamily { get; init; }
    /// <summary>図形の内側の塗り。</summary>
    public Brush? Fill { get; init; }
    /// <summary>図形の輪郭の塗り。</summary>
    public Brush? Stroke { get; init; }
    /// <summary>図形の輪郭の太さ。</summary>
    public Px? StrokeThickness { get; init; }
    /// <summary>図形の輪郭の線の端の形。始点と終点の両方に当てる。</summary>
    public PenLineCap? StrokeLineCap { get; init; }
    /// <summary>図形の輪郭の線が折れる角の形。</summary>
    public PenLineJoin? StrokeLineJoin { get; init; }

    /// <summary>この Style に、<paramref name="overlay"/> の null でない項目を上書きした新しい Style を返す。</summary>
    public Style With(Style overlay) => new()
    {
        Width = overlay.Width ?? Width,
        Height = overlay.Height ?? Height,
        MinWidth = overlay.MinWidth ?? MinWidth,
        MaxWidth = overlay.MaxWidth ?? MaxWidth,
        MinHeight = overlay.MinHeight ?? MinHeight,
        MaxHeight = overlay.MaxHeight ?? MaxHeight,
        Margin = overlay.Margin ?? Margin,
        Padding = overlay.Padding ?? Padding,
        HorizontalAlignment = overlay.HorizontalAlignment ?? HorizontalAlignment,
        VerticalAlignment = overlay.VerticalAlignment ?? VerticalAlignment,
        Orientation = overlay.Orientation ?? Orientation,
        Spacing = overlay.Spacing ?? Spacing,
        Background = overlay.Background ?? Background,
        Foreground = overlay.Foreground ?? Foreground,
        BorderBrush = overlay.BorderBrush ?? BorderBrush,
        BorderThickness = overlay.BorderThickness ?? BorderThickness,
        CornerRadius = overlay.CornerRadius ?? CornerRadius,
        FontSize = overlay.FontSize ?? FontSize,
        FontWeight = overlay.FontWeight ?? FontWeight,
        TextWrapping = overlay.TextWrapping ?? TextWrapping,
        IsTextSelectionEnabled = overlay.IsTextSelectionEnabled ?? IsTextSelectionEnabled,
        TextAlignment = overlay.TextAlignment ?? TextAlignment,
        TextTrimming = overlay.TextTrimming ?? TextTrimming,
        LineHeight = overlay.LineHeight ?? LineHeight,
        Opacity = overlay.Opacity ?? Opacity,
        RowSpacing = overlay.RowSpacing ?? RowSpacing,
        ColumnSpacing = overlay.ColumnSpacing ?? ColumnSpacing,
        HorizontalContentAlignment = overlay.HorizontalContentAlignment ?? HorizontalContentAlignment,
        VerticalContentAlignment = overlay.VerticalContentAlignment ?? VerticalContentAlignment,
        FontFamily = overlay.FontFamily ?? FontFamily,
        Fill = overlay.Fill ?? Fill,
        Stroke = overlay.Stroke ?? Stroke,
        StrokeThickness = overlay.StrokeThickness ?? StrokeThickness,
        StrokeLineCap = overlay.StrokeLineCap ?? StrokeLineCap,
        StrokeLineJoin = overlay.StrokeLineJoin ?? StrokeLineJoin,
    };

    internal WinUI.Media.Brush? CachedBackground() => Background is { } 塗り ? 背景のブラシ ??= 塗り.ToWinUI() : null;

    internal WinUI.Media.Brush? CachedForeground() => Foreground is { } 塗り ? 文字のブラシ ??= 塗り.ToWinUI() : null;

    internal WinUI.Media.Brush? CachedBorderBrush() => BorderBrush is { } 塗り ? 枠線のブラシ ??= 塗り.ToWinUI() : null;

    internal WinUI.Media.Brush? CachedFill() => Fill is { } 塗り ? 図形のブラシ ??= 塗り.ToWinUI() : null;

    internal WinUI.Media.Brush? CachedStroke() => Stroke is { } 塗り ? 輪郭のブラシ ??= 塗り.ToWinUI() : null;

    internal WinUI.Media.FontFamily? CachedFontFamily() => FontFamily is { } 名前 ? フォント ??= new WinUI.Media.FontFamily(名前) : null;

    internal void ThrowIfUnsupported(StyleItems 持つ項目, Node 部品)
    {
        StyleItems 持たない項目 = SpecifiedItems() & ~持つ項目;
        if (持たない項目 != StyleItems.None)
        {
            throw new InvalidOperationException($"{部品.GetType().Name} は Style の {持たない項目} を持たない");
        }
    }

    // 注意: 項目を足すときは、ここと With にも足す。足さないと、その項目を持たない部品へ当てても例外にならない
    private StyleItems SpecifiedItems() =>
        (Width is null ? StyleItems.None : StyleItems.Width)
        | (Height is null ? StyleItems.None : StyleItems.Height)
        | (MinWidth is null ? StyleItems.None : StyleItems.MinWidth)
        | (MaxWidth is null ? StyleItems.None : StyleItems.MaxWidth)
        | (MinHeight is null ? StyleItems.None : StyleItems.MinHeight)
        | (MaxHeight is null ? StyleItems.None : StyleItems.MaxHeight)
        | (Margin is null ? StyleItems.None : StyleItems.Margin)
        | (HorizontalAlignment is null ? StyleItems.None : StyleItems.HorizontalAlignment)
        | (VerticalAlignment is null ? StyleItems.None : StyleItems.VerticalAlignment)
        | (Opacity is null ? StyleItems.None : StyleItems.Opacity)
        | (Padding is null ? StyleItems.None : StyleItems.Padding)
        | (Orientation is null ? StyleItems.None : StyleItems.Orientation)
        | (Spacing is null ? StyleItems.None : StyleItems.Spacing)
        | (Background is null ? StyleItems.None : StyleItems.Background)
        | (Foreground is null ? StyleItems.None : StyleItems.Foreground)
        | (BorderBrush is null ? StyleItems.None : StyleItems.BorderBrush)
        | (BorderThickness is null ? StyleItems.None : StyleItems.BorderThickness)
        | (CornerRadius is null ? StyleItems.None : StyleItems.CornerRadius)
        | (FontSize is null ? StyleItems.None : StyleItems.FontSize)
        | (FontWeight is null ? StyleItems.None : StyleItems.FontWeight)
        | (TextWrapping is null ? StyleItems.None : StyleItems.TextWrapping)
        | (IsTextSelectionEnabled is null ? StyleItems.None : StyleItems.IsTextSelectionEnabled)
        | (TextAlignment is null ? StyleItems.None : StyleItems.TextAlignment)
        | (TextTrimming is null ? StyleItems.None : StyleItems.TextTrimming)
        | (LineHeight is null ? StyleItems.None : StyleItems.LineHeight)
        | (RowSpacing is null ? StyleItems.None : StyleItems.RowSpacing)
        | (ColumnSpacing is null ? StyleItems.None : StyleItems.ColumnSpacing)
        | (HorizontalContentAlignment is null ? StyleItems.None : StyleItems.HorizontalContentAlignment)
        | (VerticalContentAlignment is null ? StyleItems.None : StyleItems.VerticalContentAlignment)
        | (FontFamily is null ? StyleItems.None : StyleItems.FontFamily)
        | (Fill is null ? StyleItems.None : StyleItems.Fill)
        | (Stroke is null ? StyleItems.None : StyleItems.Stroke)
        | (StrokeThickness is null ? StyleItems.None : StyleItems.StrokeThickness)
        | (StrokeLineCap is null ? StyleItems.None : StyleItems.StrokeLineCap)
        | (StrokeLineJoin is null ? StyleItems.None : StyleItems.StrokeLineJoin);
}
