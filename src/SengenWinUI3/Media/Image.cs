using SengenWinUI3.State;
using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>画像を見せる部品。出どころはファイルのパスか ms-appx の Uri で渡す。作る関数は <see cref="UI.Image(Uri, Style?)"/> ほか。</summary>
public sealed class Image : Node
{
    private readonly WinUI.Controls.Image 本物 = new();
    private readonly Func<Uri, WinUI.Media.Imaging.BitmapImage> 画像を読み込む; // 読み込む大きさの決め方は、作るときに決まって変わらない
    private readonly bool セルに結んだか;
    private Uri? 見せている出どころ;

    internal Image(Uri source, Func<Uri, WinUI.Media.Imaging.BitmapImage> 画像を読み込む)
    {
        this.画像を読み込む = 画像を読み込む;
        出どころを見せる(source);
    }

    internal Image(IReadOnlyCell<Uri> source, Func<Uri, WinUI.Media.Imaging.BitmapImage> 画像を読み込む)
    {
        this.画像を読み込む = 画像を読み込む;
        セルに結んだか = true;
        SubscribeCell(source, 出どころを見せる);
    }

    internal override WinUI.FrameworkElement Element => 本物;

    /// <summary>見せる画像の出どころを替える。セルに結んだ Image へ呼ぶと、値の出どころが2つになるので例外を投げる。</summary>
    public Image SetSource(Uri source)
    {
        ThrowIfDestroyed();
        if (セルに結んだか)
        {
            throw new InvalidOperationException("セルに結んだ Image の出どころは、セルの値を変えて替える");
        }
        出どころを見せる(source);
        return this;
    }

    internal override void ApplyStyle(Style style)
    {
        style.ThrowIfUnsupported(StyleItems.Layout, this);
        style.ApplyLayout(本物);
    }

    private void 出どころを見せる(Uri 出どころ)
    {
        if (出どころ == 見せている出どころ)
        {
            return;
        }
        見せている出どころ = 出どころ;
        本物.Source = 画像を読み込む(出どころ);
    }
}
