using SengenWinUI3.State;

namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>画像を元の大きさで読み込んで見せる <see cref="SengenWinUI3.Image"/> を作る。</summary>
    public static Image Image(Uri source, Style? style = null) => StyleIfGiven(new Image(source, new ImageOriginalSize()), style);

    /// <summary>画像を <paramref name="decodeSize"/> の大きさで読み込んで見せる <see cref="SengenWinUI3.Image"/> を作る。</summary>
    public static Image Image(Uri source, ImageDecodeSize decodeSize, Style? style = null) => StyleIfGiven(new Image(source, decodeSize), style);

    /// <summary>セルの出どころの画像を見せ、セルが変わるたびに読み込み直す <see cref="SengenWinUI3.Image"/> を作る。</summary>
    public static Image Image(IReadOnlyCell<Uri> source, Style? style = null) => StyleIfGiven(new Image(source, new ImageOriginalSize()), style);

    /// <summary>セルの出どころの画像を <paramref name="decodeSize"/> の大きさで読み込んで見せる <see cref="SengenWinUI3.Image"/> を作る。</summary>
    public static Image Image(IReadOnlyCell<Uri> source, ImageDecodeSize decodeSize, Style? style = null) => StyleIfGiven(new Image(source, decodeSize), style);
}
