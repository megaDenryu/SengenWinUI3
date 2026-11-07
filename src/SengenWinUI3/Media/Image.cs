using SengenWinUI3.State;
using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>画像を見せる部品。出どころはファイルのパスか ms-appx の Uri で渡す。作る関数は <see cref="UI.Image(Uri, Style?)"/> ほか。</summary>
public sealed class Image : Node
{
    private static readonly StyleTarget<WinUI.Controls.Image> 当て方 = new(FrameworkElementStyle.Layout);

    private readonly WinUI.Controls.Image 本物 = new();
    private readonly ImageDecodeSize 読み込む大きさ;
    private readonly BindableValue<Image, Uri> 出どころ;

    internal Image(Uri source, ImageDecodeSize decodeSize) : this(decodeSize) => 出どころ.Set(source);

    internal Image(IReadOnlyCell<Uri> source, ImageDecodeSize decodeSize) : this(decodeSize) => 出どころ.Bind(source);

    private Image(ImageDecodeSize decodeSize)
    {
        読み込む大きさ = decodeSize;
        出どころ = new(this, this, static (部品, 見せる出どころ) => 部品.見せている出どころか(見せる出どころ), static (部品, 見せる出どころ) => 部品.Set出どころ(見せる出どころ));
    }

    internal override WinUI.FrameworkElement Element => 本物;

    /// <summary>見せる画像の出どころを替える。セルに結んだ Image へ呼ぶと、値の出どころが2つになるので例外を投げる。</summary>
    public Image SetSource(Uri source)
    {
        ThrowIfDestroyed();
        出どころ.Set(source);
        return this;
    }

    internal override void ApplyStyle(Style style) => 当て方.Apply(style, 本物, this);

    private bool 見せている出どころか(Uri 見せる出どころ) => 本物.Source is WinUI.Media.Imaging.BitmapImage 画像 && 画像.UriSource == 見せる出どころ;

    private void Set出どころ(Uri 見せる出どころ) => 本物.Source = Read画像(見せる出どころ);

    private WinUI.Media.Imaging.BitmapImage Read画像(Uri 見せる出どころ) => 読み込む大きさ switch
    {
        ImageOriginalSize => new WinUI.Media.Imaging.BitmapImage(見せる出どころ),
        ImageDecodeWidth 幅 => new WinUI.Media.Imaging.BitmapImage
        {
            DecodePixelType = WinUI.Media.Imaging.DecodePixelType.Logical,
            DecodePixelWidth = Convert.ToInt32(幅.Width.Value), // 注意: 読み込む大きさを UriSource より先に決める。後にすると元の大きさで読み込んでから縮める
            UriSource = 見せる出どころ,
        },
    };
}
