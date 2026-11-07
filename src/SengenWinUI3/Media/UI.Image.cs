using SengenWinUI3.State;
using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>画像を元の大きさで読み込んで見せる <see cref="SengenWinUI3.Image"/> を作る。</summary>
    public static Image Image(Uri source, Style? style = null) => StyleIfGiven(new Image(source, 元の大きさで読み込む), style);

    /// <summary>画像を幅 <paramref name="decodeWidth"/> に縮めて読み込み、見せる <see cref="SengenWinUI3.Image"/> を作る。小さく見せる大きな画像のメモリを減らすために使う。</summary>
    public static Image Image(Uri source, Px decodeWidth, Style? style = null) => StyleIfGiven(new Image(source, 幅を決めて読み込む(decodeWidth)), style);

    /// <summary>セルの出どころの画像を見せ、セルが変わるたびに読み込み直す <see cref="SengenWinUI3.Image"/> を作る。</summary>
    public static Image Image(IReadOnlyCell<Uri> source, Style? style = null) => StyleIfGiven(new Image(source, 元の大きさで読み込む), style);

    /// <summary>セルの出どころの画像を幅 <paramref name="decodeWidth"/> に縮めて読み込む <see cref="SengenWinUI3.Image"/> を作る。</summary>
    public static Image Image(IReadOnlyCell<Uri> source, Px decodeWidth, Style? style = null) => StyleIfGiven(new Image(source, 幅を決めて読み込む(decodeWidth)), style);

    private static WinUI.Media.Imaging.BitmapImage 元の大きさで読み込む(Uri 出どころ) => new(出どころ);

    private static Func<Uri, WinUI.Media.Imaging.BitmapImage> 幅を決めて読み込む(Px 幅) => 出どころ => new()
    {
        DecodePixelType = WinUI.Media.Imaging.DecodePixelType.Logical,
        DecodePixelWidth = Convert.ToInt32(幅.Value), // 注意: 読み込む大きさを UriSource より先に決める。後にすると元の大きさで読み込んでから縮める
        UriSource = 出どころ,
    };
}
